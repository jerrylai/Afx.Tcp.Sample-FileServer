using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using AfxTcpFileServerSample.Common;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.IService;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Dto.Repository;

namespace AfxTcpFileServerSample.Service
{
    public class FileService : BaseService, IFileService
    {
        internal readonly static FileService Instance = new FileService();

        public virtual bool CreateFile(CreateFileParamDto vm, out FileDataParamDto fileDataParam)
        {
            bool result = false;
            fileDataParam = null;
            UserInfo user = SessionUtils.UserInfo;
            if (user.File.OpenType == FileOpenType.None && !string.IsNullOrEmpty(vm.Name)
                && vm.CreationTime != DateTime.MinValue && vm.LastWriteTime != DateTime.MinValue)
            {
                DateTime createtime = vm.CreationTime;
                DateTime lasttime = vm.LastWriteTime;
                if(ConfigUtils.DatabaseType == DatabaseType.Oracle)
                {
                    createtime = DateTime.Parse(vm.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    lasttime = DateTime.Parse(vm.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                string tempFullName = null;
                var repository = this.GetRepository<ITempFileRepository>();
                var tempDto = repository.Get(vm.Directory, vm.Name);
                if (tempDto == null || tempDto.Length == 0 || tempDto.Length != vm.Length
                    || tempDto.CreationTime != createtime || tempDto.LastWriteTime != lasttime
                    || !File.Exists(Utils.Combine(ConfigUtils.TempDirectory, tempDto.TempName)))
                {
                    if (tempDto != null)
                    {
                        repository.Delete(tempDto.Id);
                        tempFullName = Utils.Combine(ConfigUtils.TempDirectory, tempDto.TempName);
                        if (File.Exists(tempFullName))
                        {
                            try { File.Delete(tempFullName); }
                            catch { }
                        }
                    }

                    tempDto = new TempFileDto()
                    {
                        TempIndex = 0,
                        TempName = Guid.NewGuid().ToString("n") + ".t"
                    };
                }

                FileStream fs = null;
                tempFullName = Utils.Combine(ConfigUtils.TempDirectory, tempDto.TempName);
                try
                {
                    if (File.Exists(tempFullName))
                    {
                        fs = File.OpenWrite(tempFullName);
                        if (tempDto.TempIndex > 0) fs.Seek(tempDto.TempIndex, SeekOrigin.Begin);
                    }
                    else
                    {
                        if (!Directory.Exists(ConfigUtils.TempDirectory)) Directory.CreateDirectory(ConfigUtils.TempDirectory);
                        fs = File.Create(tempFullName);
                        if (vm.Length > 0) fs.SetLength(vm.Length);
                    }

                    //FileInfoRepository.Instance.Delete(vm.Directory, vm.Name, (int)FileInfoType.File);
                    //string fullName = PathUtils.Combine(ConfigUtils.FileRootPath, user.File.Directory, user.File.Name);
                    //if (File.Exists(fullName)) File.Delete(fullName);

                    user.File.Directory = vm.Directory;
                    user.File.Name = vm.Name;
                    user.File.Length = vm.Length;
                    user.File.CreationTime = vm.CreationTime;
                    user.File.LastWriteTime = vm.LastWriteTime;
                    user.File.Position = fs.Position;
                    user.File.TempName = tempDto.TempName;
                    user.File.Stream = fs;
                    user.File.OpenType = FileOpenType.Write;

                    fileDataParam = new FileDataParamDto()
                    {
                        Index = user.File.Position,
                        Size = ConfigUtils.MAX_FILE_DATA_SIZE
                    };
                    result = true;

                    string log = string.Format("创建上传文件, Directory: {0}, Name: {1}, Length: {2}, Position: {3}, CreationTime: {4:yyyy-MM-dd HH:mm:ss}, LastWriteTime: {5:yyyy-MM-dd HH:mm:ss}", user.File.Directory, user.File.Name, user.File.Length, user.File.Position, user.File.CreationTime, user.File.LastWriteTime);
                   new OptionLogService().Add(OptionLogType.CreateFile, log);
                }
                catch (Exception ex)
                {
                    try
                    {
                        if (fs != null) fs.Close();
                        if (tempDto.Id == 0)
                        {
                            File.Delete(tempFullName);
                        }
                    }
                    catch { }
                    throw ex;
                }
            }

            return result;
        }

        public virtual bool UploadFileData(FileDataDto vm, out FileDataParamDto fileDataParam)
        {
            bool result = false;
            fileDataParam = null;
            UserInfo user = SessionUtils.UserInfo;
            if (vm != null && user.File.OpenType == FileOpenType.Write && user.File.Stream != null)
            {
                if (vm.Data != null && vm.Data.Length > 0 && vm.Length >= vm.Data.Length)
                {
                    user.File.Stream.Seek(vm.Index, SeekOrigin.Begin);
                    user.File.Stream.Write(vm.Data, 0, vm.Length);
                    user.File.Stream.Flush();
                }

                user.File.Position = user.File.Stream.Position;
                fileDataParam = new FileDataParamDto()
                {
                    Index = user.File.Position,
                    Size = ConfigUtils.MAX_FILE_DATA_SIZE
                };
                result = true;
            }

            return result;
        }

        public virtual bool CreateFileCompleted()
        {
            bool result = false;
            UserInfo user = SessionUtils.UserInfo;
            if (user != null && user.File.OpenType == FileOpenType.Write)
            {
                if (user.File.Length > 0 && user.File.Position < user.File.Length)
                {
                    result = this.CloseCreateFile();
                }
                else
                {
                    if (user.File.Stream != null)
                    {
                        try
                        {
                            user.File.Stream.Flush();
                            user.File.Stream.Close();
                        }
                        catch { }
                        user.File.Stream = null;
                    }

                    try
                    {
                        if (!Directory.Exists(Utils.Combine(ConfigUtils.FileRootPath, user.File.Directory)))
                        {
                            Directory.CreateDirectory(Utils.Combine(ConfigUtils.FileRootPath, user.File.Directory));
                        }

                        string fullName = Utils.Combine(ConfigUtils.FileRootPath, user.File.Directory, user.File.Name);
                        if (File.Exists(fullName)) File.Delete(fullName);
                        File.Move(Utils.Combine(ConfigUtils.TempDirectory, user.File.TempName), fullName);

                        File.SetCreationTime(fullName, user.File.CreationTime);
                        File.SetLastAccessTime(fullName, user.File.LastWriteTime);
                    }
                    catch 
                    {
                        this.CloseCreateFile();
                        return result;
                    }

                    FileInfoDto m = new FileInfoDto()
                    {
                        Type = FileInfoType.File,
                        Directory = user.File.Directory,
                        Name = user.File.Name,
                        Length = user.File.Position,
                        CreationTime = user.File.CreationTime,
                        LastWriteTime = user.File.LastWriteTime
                    };
                    
                    var parentDto = FileInfoService.Instance.GetParent(user.File.Directory);
                    if (parentDto != null) m.ParentId = parentDto.Id;

                    var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
                    fileInfoRepository.AddOrUpdate(m);

                    string log = string.Format("上传文件完成, Directory: {0}, Name: {1}, Length: {2}, Position: {3}, CreationTime: {4:yyyy-MM-dd HH:mm:ss}, LastWriteTime: {5:yyyy-MM-dd HH:mm:ss}", user.File.Directory, user.File.Name, user.File.Length, user.File.Position, user.File.CreationTime, user.File.LastWriteTime);
                    OptionLogService.Instance.Add(OptionLogType.CreateFileCompleted, log);

                    result = true;
                    user.File.Dispose();
                }
            }

            return result;
        }

        public virtual bool CloseCreateFile()
        {
            bool result = false;
            UserInfo user = SessionUtils.UserInfo;
            if (user != null && user.File.OpenType == FileOpenType.Write)
            {
                try
                {
                    TempFileDto tempDto = new TempFileDto()
                    {
                        Directory = user.File.Directory,
                        Name = user.File.Name,
                        Length = user.File.Length,
                        CreationTime = user.File.CreationTime,
                        LastWriteTime = user.File.LastWriteTime,
                        TempIndex = user.File.Position,
                        TempName = user.File.TempName
                    };

                    var tempFileRepository = this.GetRepository<ITempFileRepository>();
                    tempFileRepository.AddOrUpdate(tempDto);

                    string log = string.Format("关闭上传文件, Directory: {0}, Name: {1}, Length: {2}, Position: {3}, CreationTime: {4:yyyy-MM-dd HH:mm:ss}, LastWriteTime: {5:yyyy-MM-dd HH:mm:ss}", user.File.Directory, user.File.Name, user.File.Length, user.File.Position, user.File.CreationTime, user.File.LastWriteTime);
                    OptionLogService.Instance.Add(OptionLogType.CloseCreateFile, log);

                    result = true;
                }
                catch { }
                user.File.Dispose();
            }

            return result;
        }

        public virtual bool CloseFile()
        {
            bool result = false;
            UserInfo user = SessionUtils.UserInfo;
            if (user != null && user.File.OpenType != FileOpenType.None)
            {
                if (user.File.OpenType == FileOpenType.Read)
                {
                    string log = string.Format("关闭下载文件, Directory: {0}, Name: {1}", user.File.Directory, user.File.Name);
                    OptionLogService.Instance.Add(OptionLogType.CloseOpenFile, log);
                    result = true;
                }
                else if (user.File.OpenType == FileOpenType.Write)
                {
                    result = this.CloseCreateFile();
                }

                user.File.Dispose();
            }

            return result;
        }

        public virtual bool OpenFile(int id)
        {
            bool result = false;
            UserInfo user = SessionUtils.UserInfo;
            if (user != null && id > 0 && user.File.OpenType == FileOpenType.None)
            {
                var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
                var m = fileInfoRepository.Get(id);
                if (m != null && m.Type == FileInfoType.File)
                {
                    string fullname = Utils.Combine(ConfigUtils.FileRootPath, m.Directory, m.Name);
                    if (File.Exists(fullname))
                    {
                        user.File.Stream = File.OpenRead(fullname);
                        user.File.OpenType = FileOpenType.Read;
                        user.File.Directory = m.Directory;
                        user.File.Name = m.Name;
                        user.File.Length = m.Length;
                        user.File.Position = 0;
                        user.File.CreationTime = m.CreationTime;
                        user.File.LastWriteTime = m.LastWriteTime;
                        result = true;

                        string log = string.Format("打开下载文件, Directory: {0}, Name: {1}, Length: {2}, Position: {3}, CreationTime: {4:yyyy-MM-dd HH:mm:ss}, LastWriteTime: {5:yyyy-MM-dd HH:mm:ss}", user.File.Directory, user.File.Name, user.File.Length, user.File.Position, user.File.CreationTime, user.File.LastWriteTime);
                        OptionLogService.Instance.Add(OptionLogType.OpenFile, log);
                    }
                }
            }

            return result;
        }

        public virtual FileDataDto GetFileData(FileDataParamDto vm)
        {
            FileDataDto m = null;
            UserInfo user = SessionUtils.UserInfo;
            if (user != null && vm != null && user.File.OpenType == FileOpenType.Read && user.File.Stream != null)
            {
                user.File.Stream.Seek(vm.Index, SeekOrigin.Begin);
                m = new FileDataDto() { Index = vm.Index };
                long len = user.File.Stream.Length - user.File.Position;
                if(vm.Size > len)
                {
                    vm.Size = Convert.ToInt32(len);
                }
                m.Data = new byte[vm.Size];
                m.Length = user.File.Stream.Read(m.Data, 0, vm.Size);
            }

            return m;
        }

        public virtual bool CreateDirectory(CreateDirectoryParamDto vm)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(vm.Name) && vm.CreationTime != DateTime.MinValue && vm.LastWriteTime != DateTime.MinValue)
            {
                string fullname = Utils.Combine(ConfigUtils.FileRootPath, vm.Directory, vm.Name);
                if (!Directory.Exists(fullname))
                {
                    Directory.CreateDirectory(fullname);
                    Directory.SetCreationTime(fullname, vm.CreationTime);
                    Directory.SetLastAccessTime(fullname, vm.LastWriteTime);
                }

                var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
                var m = fileInfoRepository.Get(vm.Directory, vm.Name, FileInfoType.Directory);
                if (m == null)
                {
                    m = new FileInfoDto()
                    {
                        Type = FileInfoType.Directory,
                        Directory = vm.Directory,
                        Name = vm.Name,
                        CreationTime = vm.CreationTime,
                        LastWriteTime = vm.LastWriteTime,
                        IsDelete = false,
                    };
                    var parent = FileInfoService.Instance.GetParent(vm.Directory);
                    if (parent != null) m.ParentId = parent.Id;
                    fileInfoRepository.AddOrUpdate(m);
                }
                result = true;

                string log = string.Format("创建目录, Directory: {0}, Name: {1}, CreationTime: {2:yyyy-MM-dd HH:mm:ss}, LastWriteTime: {3:yyyy-MM-dd HH:mm:ss}", m.Directory, m.Name, m.Length, m.CreationTime, m.LastWriteTime);
                OptionLogService.Instance.Add(OptionLogType.CreateDirectory, log);
            }

            return result;
        }

        public virtual bool DeleteOpenFile()
        {
            bool result = false;
            UserInfo user = SessionUtils.UserInfo;
            if (user != null && user.File.OpenType != FileOpenType.None)
            {
                if (user.File.Stream != null)
                {
                    try
                    {
                        user.File.Stream.Flush();
                        user.File.Stream.Close();
                    }
                    catch { }
                    user.File.Stream = null;
                }

                string log = string.Format("删除打开文件, Directory: {0}, Name: {1}", user.File.Directory, user.File.Name);
                string fullname = Utils.Combine(ConfigUtils.FileRootPath, user.File.Directory, user.File.Name);
                if (user.File.OpenType == FileOpenType.Read)
                {
                    var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
                    fileInfoRepository.Delete(user.File.Directory, user.File.Name, FileInfoType.File);
                }
                else if (user.File.OpenType == FileOpenType.Write)
                {
                    fullname = Utils.Combine(ConfigUtils.TempDirectory, user.File.TempName);
                    var tempFileRepository = this.GetRepository<ITempFileRepository>();
                    tempFileRepository.Delete(user.File.Directory, user.File.Name);
                }
                File.Delete(fullname);
                result = true;
                
                OptionLogService.Instance.Add(OptionLogType.DeleteOpenFile, log);
                user.File.Dispose();
            }

            return result;
        }

        public virtual string GetPhysicaPath(string path)
        {
            string s = Utils.Combine(ConfigUtils.FileRootPath, path);

            return s;
        }

        public virtual bool AddFileInfo(AddFileInfoDto vm)
        {
            bool result = false;
            if (vm != null && !string.IsNullOrEmpty(vm.Name))
            {
                string fullname = Utils.Combine(ConfigUtils.FileRootPath, vm.Directory, vm.Name);
                if (vm.Type == FileInfoType.File && File.Exists(fullname)
                    || vm.Type == FileInfoType.Directory && Directory.Exists(vm.Directory))
                {
                    FileInfoDto m = new FileInfoDto()
                    {
                        Type = vm.Type,
                        Directory = vm.Directory,
                        Name = vm.Name,
                        IsDelete = false
                    };
                    if(vm.Type == FileInfoType.File)
                    {
                        var info = new FileInfo(fullname);
                        m.CreationTime = info.CreationTime;
                        m.LastWriteTime = info.LastAccessTime;
                        m.Length = info.Length;
                    }
                    else
                    {
                        var info = new DirectoryInfo(fullname);
                        m.CreationTime = info.CreationTime;
                        m.LastWriteTime = info.LastAccessTime;
                    }
                    var parent = FileInfoService.Instance.GetParent(vm.Directory);
                    if (parent != null) m.ParentId = parent.ParentId;
                    var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
                    fileInfoRepository.AddOrUpdate(m);
                    result = true;
                }
            }

            return result;
        }
    }
}
