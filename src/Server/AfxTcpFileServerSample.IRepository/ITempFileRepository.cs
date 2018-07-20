using AfxTcpFileServerSample.Dto.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.IRepository
{
    public interface ITempFileRepository : IBaseRepository
    {
        TempFileDto Get(string directory, string name);

        int AddOrUpdate(TempFileDto vm);

        int Delete(int id);

        int Delete(string directory, string name);
    }
}
