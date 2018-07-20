using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AfxTcpFileServerSample.IRepository
{
    public interface IFileInfoRepository : IBaseRepository
    {
        int AddOrUpdate(FileInfoDto vm);

        int AddOrUpdate(List<FileInfoDto> list);

        FileInfoDto Get(int id);

        FileInfoDto Get(string directory, string name, FileInfoType type);

        int Delete(int id);

        int Delete(string directory, string name, FileInfoType type);

        bool Exist(string directory, string name, FileInfoType type);

        PageListDto<FileInfoDto> GetPageList(FileInfoPageParamDto vm);

        List<FileInfoDto> GetSyncList(SyncParamDto vm);

        int UpdateSync(FileInfoDto vm);

        int ResetCheckStatus();

        List<FileInfoDto> GetCheckStatusList(int count);

        int DeleteCheckStatus();

        int UpdateCheckStatus(int id);
    }
}
