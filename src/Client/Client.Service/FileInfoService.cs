using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using AfxTcpFileServerSample.Dto.ProtoBuf;
using Client.IService;
using AfxTcpFileServerSample.Enums;
using Client.Common;

namespace Client.Service
{
    public class FileInfoService : BaseService, IFileInfoService
    {
        public const int MAX_FILE_DATA_SIZE = 7 * 1024;
        public const string FILE_TEMP_SUFFIX = ".t";
        public const string FILE_INDEX_SUFFIX = ".p";
        
        public FileInfoService(IFileClient client)
            : base(client)
        {
        }

        public virtual PageListDto<FileInfoDto> GetPageList(FileInfoPageParamDto vm)
        {
            PageListDto<FileInfoDto> result = null;
            if(vm != null && this.Client.IsLogin)
            {
                result = this.Client.Request<PageListDto<FileInfoDto>, FileInfoPageParamDto>(MsgCmd.GetFileInfoPageList, vm);
            }

            return result;
        }

        private bool OnFilePositionCallback(FilePositionCallback call, long length, long position)
        {
            bool result = true;
            if (call != null)
            {
                try { result = call(length, position); }
                catch {}
            }

            return result;
        }

        public virtual string GetLocalPath(string filePath)
        {
            string path = null;
            if (this.Client.IsLogin)
            {
                path = this.Client.Request<string, string>(MsgCmd.GetPhysicaPath, filePath);
            }

            return path;
        }

        private bool AddFileInfo(FileInfo localinfo, string remoteDirectory, string remoteName)
        {
            bool result = false;
            string serverfile = this.GetLocalPath(Utils.Combine(remoteDirectory, remoteName));
            if (!string.IsNullOrEmpty(serverfile))
            {
                string dir = Path.GetDirectoryName(serverfile);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                if (File.Exists(serverfile))
                {
                    var serverinfo = new FileInfo(serverfile);
                    if (serverinfo.Length != localinfo.Length
                        || serverinfo.CreationTime != localinfo.CreationTime
                        || serverinfo.LastAccessTime != localinfo.LastAccessTime)
                    {
                        localinfo.CopyTo(serverfile, true);
                    }
                }
                else
                {
                    localinfo.CopyTo(serverfile, true);
                }
                AddFileInfoDto vm = new AddFileInfoDto()
                {
                    Type = FileInfoType.File,
                    Directory = remoteDirectory,
                    Name = remoteName
                };
                result = this.Client.Request<bool, AddFileInfoDto>(MsgCmd.AddFileInfo, vm);
            }

            return result;
        }

        public virtual bool SendFile(string localFullFileName, string remoteDirectory, string remoteName, FilePositionCallback call = null)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(localFullFileName) && !string.IsNullOrEmpty(remoteName) 
                    && File.Exists(localFullFileName) && this.Client.IsLogin)
                {
                    var localinfo = new FileInfo(localFullFileName);
                    this.OnFilePositionCallback(call, localinfo.Length, 0);
                    if (this.Client.Host == "127.0.0.1" || this.Client.Host == "localhost")
                    {
                        result = this.AddFileInfo(localinfo, remoteDirectory, remoteName);
                        this.OnFilePositionCallback(call, localinfo.Length, localinfo.Length);
                    }
                    else
                    {
                        using (var fs = File.OpenRead(localFullFileName))
                        {
                            long fileLength = fs.Length;
                            CreateFileParamDto createDto = new CreateFileParamDto()
                            {
                                Directory = remoteDirectory,
                                Name = remoteName,
                                Length = localinfo.Length,
                                CreationTime = localinfo.CreationTime,
                                LastWriteTime = localinfo.LastAccessTime
                            };
                            var getDto = this.Client.Request<FileDataParamDto, CreateFileParamDto>(MsgCmd.CreateFile, createDto);
                            FileDataDto dataDto = new FileDataDto();
                            if (getDto != null)
                            {
                                while (getDto.Index < fileLength)
                                {
                                    dataDto.Index = getDto.Index;
                                    int size = getDto.Size;
                                    long temp = fileLength - dataDto.Index;
                                    if (temp < size) size = Convert.ToInt32(temp);
                                    byte[] buffer = new byte[size];
                                    fs.Seek(dataDto.Index, SeekOrigin.Begin);
                                    dataDto.Length = fs.Read(buffer, 0, buffer.Length);
                                    dataDto.Data = buffer;
                                    getDto = this.Client.Request<FileDataParamDto, FileDataDto>(MsgCmd.UploadFileData, dataDto);
                                    bool isContinue = this.OnFilePositionCallback(call, fileLength, dataDto.Index);
                                    if (getDto == null || !isContinue)
                                    {
                                        break;
                                    }
                                }

                                this.Client.Request(MsgCmd.CreateFileCompleted);

                                if (getDto != null && getDto.Index >= fileLength)
                                {
                                    this.OnFilePositionCallback(call, fileLength, dataDto.Index);
                                    result = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtils.Error("【FileInfoClient.SendFile】", ex);
            }

            return result;
        }

        private bool GetLocalFile(FileInfoDto m, string saveFullDirectory, FilePositionCallback call = null)
        {
            bool result = false;
            string serverfile = this.GetLocalPath(Utils.Combine(m.Directory, m.Name));
            if (File.Exists(serverfile))
            {
                var serverinfo = new FileInfo(serverfile);
                this.OnFilePositionCallback(call, serverinfo.Length, 0);
                string localFullFileName = Utils.Combine(saveFullDirectory, m.Name);
                if (File.Exists(localFullFileName))
                {
                    var localInfo = new FileInfo(localFullFileName);
                    if (serverinfo.Length != localInfo.Length || serverinfo.CreationTime != localInfo.CreationTime || serverinfo.LastAccessTime != localInfo.LastAccessTime)
                    {
                        serverinfo.CopyTo(localFullFileName, true);
                    }
                }
                else
                {
                    serverinfo.CopyTo(localFullFileName, true);
                }
                this.OnFilePositionCallback(call, serverinfo.Length, serverinfo.Length);
                result = true;
            }

            return result;
        }

        private FileStream CreateFile(FileInfoDto m, string saveFullDirectory, string name, out long position)
        {
            position = 0;
            FileStream fs = null;
            string userFile = Utils.Combine(saveFullDirectory, name);
            if (m.Length == 0)
            {
                using (fs = File.Create(userFile))
                {
                    fs.Flush();
                    fs.Close();
                }
                fs = null;
                File.SetCreationTime(userFile, m.CreationTime);
                File.SetLastAccessTime(userFile, m.LastWriteTime);

                return fs;
            }

            if (File.Exists(userFile))
            {
                FileInfo info = new FileInfo(userFile);
                if (info.Length == m.Length
                    && info.CreationTime.ToString("yyyy-MM-dd HH:mm:ss") == m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")
                    && m.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") == m.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"))
                {
                    position = m.Length;

                    return fs;
                }
            }

            string saveFile = Utils.Combine(saveFullDirectory, m.Name);
            string tempFile = saveFile + FILE_TEMP_SUFFIX;
            string indexFile = saveFile + FILE_INDEX_SUFFIX;

            if (File.Exists(tempFile) && File.Exists(indexFile))
            {
                FileInfo info = new FileInfo(tempFile);
                string s = File.ReadAllText(indexFile, Encoding.UTF8);
                int i = s.IndexOf(' ');
                if (i > 0)
                {
                    long.TryParse(s.Substring(0, i), out position);
                    FileInfoDto tm = JsonUtils.Deserialize<FileInfoDto>(s.Substring(i + 1));
                    if (tm != null && tm.Length == m.Length && tm.CreationTime == m.CreationTime && tm.LastWriteTime == m.LastWriteTime)
                    {
                        fs = File.OpenWrite(tempFile);
                        fs.Seek(position, SeekOrigin.Begin);

                        return fs;
                    }
                }
            }

            position = 0;
            if (File.Exists(tempFile)) File.Delete(tempFile);
            if (File.Exists(indexFile)) File.Delete(indexFile);

            fs = File.Create(tempFile);
            try
            {
                fs.SetLength(m.Length);
            }
            catch (Exception ex)
            {
                fs.Dispose();
                fs = null;
                try { File.Delete(tempFile); }
                catch { }
                LogUtils.Error("【FileInfoClient.CreateFile】", ex);
            }

            return fs;
        }

        public virtual bool GetFile(FileInfoDto m, string saveFullDirectory, string name, FilePositionCallback call = null)
        {
            bool result = false;
            bool isopen = false;
            try
            {
                if (m != null && m.Type == FileInfoType.File && !string.IsNullOrEmpty(saveFullDirectory) && this.Client.IsLogin)
                {
                    if (!Directory.Exists(saveFullDirectory)) Directory.CreateDirectory(saveFullDirectory);
                    if (this.Client.Host == "127.0.0.1" || this.Client.Host == "localhost")
                    {
                        result = this.GetLocalFile(m, saveFullDirectory, call);
                    }
                    else
                    {
                        long position = 0;
                        this.OnFilePositionCallback(call, m.Length, position);
                        var fs = this.CreateFile(m, saveFullDirectory, name, out position);
                        if (m.Length == position && fs == null)
                        {
                            this.OnFilePositionCallback(call, m.Length, position);
                            result = true;
                        }
                        else if (fs != null)
                        {
                            using (fs)
                            {
                                isopen = this.Client.Request<bool, int>(MsgCmd.OpenFile, m.Id);
                                    if (isopen)
                                    {
                                        FileDataParamDto getDto = new FileDataParamDto();
                                        while (position < m.Length)
                                        {
                                            bool isContinue = this.OnFilePositionCallback(call, m.Length, position);
                                            getDto.Index = position;
                                            getDto.Size = MAX_FILE_DATA_SIZE;
                                        FileDataDto dataDto = this.Client.Request<FileDataDto, FileDataParamDto>(MsgCmd.GetFileData, getDto);
                                            if (dataDto != null && isContinue)
                                            {
                                                fs.Seek(position, SeekOrigin.Begin);
                                                if (dataDto.Length > 0 && dataDto.Data != null && dataDto.Length >= dataDto.Data.Length)
                                                {
                                                    fs.Write(dataDto.Data, 0, dataDto.Length);
                                                    fs.Flush();
                                                }
                                                position = position + dataDto.Length;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        this.Client.Request(MsgCmd.CloseFile);

                                        string saveFile = Utils.Combine(saveFullDirectory, m.Name);
                                        string tempFile = saveFile + FILE_TEMP_SUFFIX;
                                        string indexFile = saveFile + FILE_INDEX_SUFFIX;
                                        File.WriteAllText(indexFile, position.ToString() + " " + JsonUtils.Serialize(m), Encoding.UTF8);
                                        fs.Close();
                                        if (position >= m.Length)
                                        {
                                            this.OnFilePositionCallback(call, m.Length, position);
                                            if (File.Exists(saveFile)) File.Delete(saveFile);
                                            File.Move(tempFile, Utils.Combine(saveFullDirectory, name));
                                            File.Delete(indexFile);
                                            result = true;
                                        }
                                    }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (isopen) this.Client.Request(MsgCmd.CloseFile);
                LogUtils.Error("【FileInfoClient.GetFile】", ex);
            }

            return result;
        }

        public virtual FileInfoDto Get(int id)
        {
            FileInfoDto m = null;
            if (id > 0 && this.Client.IsLogin)
            {
                m = this.Client.Request<FileInfoDto,int>(MsgCmd.GetFileInfo, id);
            }

            return m;
        }

        public virtual bool Delete(int id)
        {
            bool result = false;
            if(id > 0 && this.Client.IsLogin)
            {
                result = this.Client.Request<bool, int>(MsgCmd.DeleteFileInfo, id);
            }

            return result;
        }

        public virtual bool SendDirectory(string localFullDirectoryName, string remoteDirectory, string remoteName)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(remoteName) && !string.IsNullOrEmpty(remoteName)
                    && Directory.Exists(localFullDirectoryName) && this.Client.IsLogin)
                {
                    var info = new DirectoryInfo(localFullDirectoryName);
                    CreateDirectoryParamDto craeateParam = new CreateDirectoryParamDto()
                    {
                        Directory = remoteDirectory,
                        Name = remoteName,
                        CreationTime = info.CreationTime,
                        LastWriteTime = info.LastWriteTime
                    };
                    result = this.Client.Request<bool, CreateDirectoryParamDto>(MsgCmd.CreateDirectory, craeateParam);
                }
            }
            catch(Exception ex)
            {
                LogUtils.Error("【FileInfoClient.SendDirectory】", ex);
            }

            return result;
        }

        public virtual bool GetDirectory(FileInfoDto m, string saveFullDirectory)
        {
            bool result = false;
            try
            {
                if (m != null && m.Type == FileInfoType.Directory
                    && !string.IsNullOrEmpty(m.Name) && !string.IsNullOrEmpty(saveFullDirectory))
                {
                    if (!Directory.Exists(saveFullDirectory)) Directory.CreateDirectory(saveFullDirectory);
                    var dir = Utils.Combine(saveFullDirectory, m.Directory, m.Name);
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    Directory.SetCreationTime(dir, m.CreationTime);
                    Directory.SetLastAccessTime(dir, m.LastWriteTime);

                    result = true;
                }
            }
            catch(Exception ex)
            {
                LogUtils.Error("【FileInfoClient.GetDirectory】", ex);
            }

            return result;
        }

    }
}
