using System;
using System.Collections.Generic;
using System.Text;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.IRepository
{
    public interface IServerInfoRepository : IBaseRepository
    {
        List<ServerInfoDto> GetList();

        ServerInfoDto Get(int id);

        int Add(ServerInfoDto vm);

        int Update(ServerInfoDto vm);

        int Delete(int id);

        bool Exist(string address);
    }
}
