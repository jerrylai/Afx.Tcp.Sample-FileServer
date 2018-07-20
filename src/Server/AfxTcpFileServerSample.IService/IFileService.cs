using AfxTcpFileServerSample.Dto.ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.IService
{
    public interface IFileService : IBaseService
    {
        bool CreateFile(CreateFileParamDto vm, out FileDataParamDto fileDataParam);

        bool UploadFileData(FileDataDto vm, out FileDataParamDto fileDataParam);

        bool CreateFileCompleted();

        bool CloseCreateFile();

        bool CloseFile();

        bool OpenFile(int id);

        FileDataDto GetFileData(FileDataParamDto vm);

        bool CreateDirectory(CreateDirectoryParamDto vm);

        bool DeleteOpenFile();

        string GetPhysicaPath(string path);

        bool AddFileInfo(AddFileInfoDto vm);

    }
}
