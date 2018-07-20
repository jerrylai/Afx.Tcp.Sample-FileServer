using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Dto.Service;
using AfxTcpFileServerSample.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.IService
{
    public interface IOptionLogService : IBaseService
    {
        void Add(OptionLogType type, string msg);

        void Add(OptionLogType type, List<string> list);

        void Add(List<AddOptionLogDto> list);

        PageListDto<OptionLogDto> GetPageList(OptionLogPageParamDto vm);
    }
}
