using AfxTcpFileServerSample.Dto.ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AfxTcpFileServerSample.IRepository
{
    public interface IOptionLogRepository : IBaseRepository
    {
        int Add(List<OptionLogDto> list);

        int Add(OptionLogDto vm);
        
        PageListDto<OptionLogDto> GetPageList(OptionLogPageParamDto vm);
    }
}
