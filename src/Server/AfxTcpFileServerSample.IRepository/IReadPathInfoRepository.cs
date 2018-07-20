using AfxTcpFileServerSample.Dto.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AfxTcpFileServerSample.IRepository
{
    public interface IReadPathInfoRepository : IBaseRepository
    {
        int Add(List<ReadPathDto> list);

        List<ReadPathDto> GetList(int count);

        int Delete(int id);

        int Count();
    }
}
