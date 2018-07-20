using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace Client.IService
{
    public interface IFileInfoService : IBaseService
    {
        PageListDto<FileInfoDto> GetPageList(FileInfoPageParamDto vm);

        string GetLocalPath(string filePath);

        bool SendFile(string localFullFileName, string remoteDirectory, string remoteName, FilePositionCallback call = null);

        bool GetFile(FileInfoDto m, string saveFullDirectory, string name, FilePositionCallback call = null);

        FileInfoDto Get(int id);

        bool Delete(int id);

        bool SendDirectory(string localFullDirectoryName, string remoteDirectory, string remoteName);

        bool GetDirectory(FileInfoDto m, string saveFullDirectory);
    }
}
