using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.Common;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.IService;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Dto.Repository;

namespace AfxTcpFileServerSample.Service
{
    public class FileInfoService : BaseService, IFileInfoService
    {
        internal readonly static FileInfoService Instance = new FileInfoService();

        #region
        private static bool isCheckFileInfo = false;
        public virtual void CheckFileInfo()
        {
            if (isCheckFileInfo) return;
            isCheckFileInfo = true;
            System.Threading.ThreadPool.QueueUserWorkItem(this.StartCheckFileInfo);
        }

        public virtual bool RestCheckStatus()
        {
            bool result = false;
            CheckFileInfoStatus status = this.GetCheckStatus();
            result = status == CheckFileInfoStatus.None;
            if (status == CheckFileInfoStatus.Completed)
            {
                this.SetCheckStatus(0);
                result = true;
            }

            return result;
        }

        public virtual CheckFileInfoStatus GetCheckStatus()
        {
            CheckFileInfoStatus status = CheckFileInfoStatus.None;
            var sysConfigRepository = this.GetRepository<ISysConfigRepository>();
            var list = sysConfigRepository.GetList(ConfigType.CheckFileInfoStatus);
            if(list.Count == 0)
            {
                var m = new SysConfigDto()
                {
                    Type = ConfigType.CheckFileInfoStatus,
                    Key = "CheckFileInfoStatus",
                    Value = CheckFileInfoStatus.None.ToString()
                };
                list.Add(m);
                sysConfigRepository.AddOrUpdate(list, ConfigType.CheckFileInfoStatus);
            }
            Enum.TryParse(list[0].Value, true, out status);

            return status;
        }

        private bool SetCheckStatus(CheckFileInfoStatus status)
        {
            bool result = false;
            var sysConfigRepository = this.GetRepository<ISysConfigRepository>();
            var list = sysConfigRepository.GetList(ConfigType.CheckFileInfoStatus);
            if (list.Count == 0)
            {
                var m = new SysConfigDto()
                {
                    Type = ConfigType.CheckFileInfoStatus,
                    Key = "CheckFileInfoStatus",
                    Value = CheckFileInfoStatus.None.ToString()
                };
                list.Add(m);
            }
            list[0].Value = status.ToString();
            sysConfigRepository.AddOrUpdate(list, ConfigType.CheckFileInfoStatus);

            return result;
        }
        
        private void StartCheckFileInfo(object obj)
        {
            try
            {
                var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
                CheckFileInfoStatus status = GetCheckStatus();
                if (!string.IsNullOrEmpty(ConfigUtils.FileRootPath))
                {
                    if (status == CheckFileInfoStatus.None)
                    {
                        fileInfoRepository.ResetCheckStatus();
                        status = CheckFileInfoStatus.RestCheckStatus;
                        this.SetCheckStatus(status);
                    }

                    if (status == CheckFileInfoStatus.RestCheckStatus)
                    {
                        this.ReadFileInfo("\\");
                        status = CheckFileInfoStatus.ReadRootPath;
                        this.SetCheckStatus(status);
                    }

                    if (status == CheckFileInfoStatus.ReadRootPath)
                    {
                        var readPathInfoRepository = this.GetRepository<IReadPathInfoRepository>();
                        List<ReadPathDto> list = readPathInfoRepository.GetList(20);
                        while (list.Count > 0)
                        {
                            foreach (var m in list)
                            {
                                this.ReadFileInfo(m.Path);
                                readPathInfoRepository.Delete(m.Id);
                            }
                            list = readPathInfoRepository.GetList(20);
                        }
                        status = CheckFileInfoStatus.ReadPathInfo;
                        this.SetCheckStatus(status);
                    }

                    if (status == CheckFileInfoStatus.ReadPathInfo)
                    {
                        fileInfoRepository.DeleteCheckStatus();
                        status = CheckFileInfoStatus.Completed;
                        this.SetCheckStatus(status);
                    }
                }
            }
            catch(Exception ex)
            {
                LogUtils.Error("【FileInfoService.StartCheckFileInfo】", ex);
            }
            isCheckFileInfo = false;
        }

        internal FileInfoDto GetParent(string path)
        {
            FileInfoDto vm = null;
            if (!string.IsNullOrEmpty(path) && path != "\\")
            {
                string dir = Utils.GetParent(path);
                string name = Utils.GetName(path);
                var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
                vm = fileInfoRepository.Get(dir, name, FileInfoType.Directory);
                if (vm == null)
                {
                    var parent = this.GetParent(dir);
                    string fullPath = Utils.Combine(ConfigUtils.FileRootPath, path);
                    DirectoryInfo dirInfo = new DirectoryInfo(fullPath);
                    vm = new FileInfoDto()
                    {
                        Type = FileInfoType.Directory,
                        Directory = path,
                        Name = dirInfo.Name,
                        CreationTime = dirInfo.CreationTime,
                        LastWriteTime = dirInfo.LastWriteTime
                    };
                    if (parent != null) vm.ParentId = parent.Id;
                    fileInfoRepository.AddOrUpdate(vm);
                }
            }

            return vm;
        }

        private void ReadFileInfo(string path)
        {
            string fullPath = Utils.Combine(ConfigUtils.FileRootPath, path);
            if (Directory.Exists(fullPath))
            {
                int parentId = 0;
                var vm = this.GetParent(path);
                if (vm != null) parentId = vm.Id;
                var dirs = Directory.EnumerateDirectories(fullPath);
                List<ReadPathDto> dirList = new List<ReadPathDto>(20);
                List<FileInfoDto> fileList = new List<FileInfoDto>(20);

                var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
                var readPathInfoRepository = this.GetRepository<IReadPathInfoRepository>();
                foreach (var item in dirs)
                {
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(item);
                        ReadPathDto read = new ReadPathDto()
                        {
                            Path = Utils.GetRelative(item, ConfigUtils.FileRootPath)
                        };
                        dirList.Add(read);

                        FileInfoDto m = new FileInfoDto()
                        {
                            ParentId = parentId,
                            Type = FileInfoType.Directory,
                            Directory = path,
                            Name = dirInfo.Name,
                            CreationTime = dirInfo.CreationTime,
                            LastWriteTime = dirInfo.LastWriteTime,
                            IsDelete = false
                        };
                        fileList.Add(m);
                    }
                    catch(Exception ex)
                    {
                        LogUtils.Debug("FileInfoService.ReadFileInfo", ex);
                    }

                    if (dirList.Count >= 20)
                    {
                        readPathInfoRepository.Add(dirList);
                        dirList.Clear();
                        fileInfoRepository.AddOrUpdate(fileList);
                        fileList.Clear();
                    }
                }

                if (dirList.Count > 0)
                {
                    readPathInfoRepository.Add(dirList);
                    dirList.Clear();
                }
                
                if (fileList.Count > 0)
                {
                    fileInfoRepository.AddOrUpdate(fileList);
                    fileList.Clear();
                }
                
                var files = Directory.EnumerateFiles(fullPath);
                foreach (var item in files)
                {
                    try
                    {
                        FileInfo finfo = new FileInfo(item);
                        FileInfoDto m = new FileInfoDto()
                        {
                            ParentId = parentId,
                            Type = FileInfoType.File,
                            Directory = path,
                            Name = finfo.Name,
                            Length = finfo.Length,
                            CreationTime = finfo.CreationTime,
                            LastWriteTime = finfo.LastWriteTime,
                            IsDelete = false
                        };
                        fileList.Add(m);
                    }
                    catch (Exception ex)
                    {
                        LogUtils.Debug("FileInfoService.ReadFileInfo", ex);
                    }

                    if (fileList.Count >= 20)
                    {
                        fileInfoRepository.AddOrUpdate(fileList);
                        fileList.Clear();
                    }
                }

                if (fileList.Count > 0)
                {
                    fileInfoRepository.AddOrUpdate(fileList);
                    fileList.Clear();
                }
            }
        }
        #endregion

        public virtual FileInfoDto Get(int id)
        {
            var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
            return fileInfoRepository.Get(id);
        }

        public virtual bool Exist(FileInfoParamDto vm)
        {
            var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
            return fileInfoRepository.Exist(vm.Directory, vm.Name, vm.Type);
        }

        private void DeleteChild(string fullPath)
        {
            var list = Directory.EnumerateFiles(fullPath);
            var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
            foreach(var f in list)
            {
                File.Delete(f);
                var dir = Utils.GetRelative(f, ConfigUtils.FileRootPath);
                var name = Utils.GetName(f);
                fileInfoRepository.Delete(dir, name, FileInfoType.File);
            }

            list = Directory.EnumerateDirectories(fullPath);
            foreach (var f in list)
            {
                this.DeleteChild(f);
                Directory.Delete(f, true);
                var dir = Utils.GetRelative(f, ConfigUtils.FileRootPath);
                var name = Utils.GetName(f);
                fileInfoRepository.Delete(dir, name, FileInfoType.Directory);
            }
        }

        public virtual bool Delete(int id)
        {
            bool result = false;
            var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
            var m = fileInfoRepository.Get(id);
            if (m != null)
            {
                string fullname = Utils.Combine(ConfigUtils.FileRootPath, m.Directory, m.Name);
                if (m.Type == FileInfoType.File)
                {
                    if (File.Exists(fullname))
                    {
                        File.Delete(fullname);
                    }
                }
                else if(m.Type == FileInfoType.Directory)
                {
                    if (Directory.Exists(fullname))
                    {
                        this.DeleteChild(fullname);
                        Directory.Delete(fullname, true);
                    }
                }
                fileInfoRepository.Delete(m.Id);
                result = true;

                string log = string.Format("删除{0}, Directory: {1}, Name: {2}", m.Type == FileInfoType.File ? "文件" : "目录", m.Directory, m.Name);

                OptionLogService.Instance.Add(m.Type == FileInfoType.File ? OptionLogType.DeleteFile : OptionLogType.DeleteDirectory, log);
            }

            return result;
        }

        public virtual List<FileInfoDto> GetSysList(SyncParamDto vm)
        {
            var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
            return fileInfoRepository.GetSyncList(vm);
        }

        public virtual PageListDto<FileInfoDto> GetPageList(FileInfoPageParamDto vm)
        {
            var fileInfoRepository = this.GetRepository<IFileInfoRepository>();
            return fileInfoRepository.GetPageList(vm);
        }
    }
}
