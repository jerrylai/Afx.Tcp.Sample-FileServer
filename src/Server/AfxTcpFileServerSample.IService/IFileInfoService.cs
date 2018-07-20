using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.IService
{
    public interface IFileInfoService : IBaseService
    {
        void CheckFileInfo();

        bool RestCheckStatus();

        CheckFileInfoStatus GetCheckStatus();

        FileInfoDto Get(int id);

        bool Exist(FileInfoParamDto vm);

        bool Delete(int id);

        List<FileInfoDto> GetSysList(SyncParamDto vm);

        PageListDto<FileInfoDto> GetPageList(FileInfoPageParamDto vm);
    }
}
