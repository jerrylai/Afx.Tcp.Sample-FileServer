using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Common;
using AfxTcpFileServerSample.Models;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.ICache;
using System.Linq.Expressions;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.Repository
{
    public abstract class BaseRepository : IBaseRepository
    {
        protected virtual FileContext CreateDbContext()
        {

            return new FileContext();
        }

        protected virtual T GetCache<T>() where T : IBaseCache => IocUtils.Get<T>();

        protected virtual T GetCache<T>(string name) where T : IBaseCache => IocUtils.Get<T>(name);

        protected virtual T GetCache<T>(object[] args) where T : IBaseCache => IocUtils.Get<T>(args);

        protected virtual T GetCache<T>(string name, object[] args) where T : IBaseCache => IocUtils.Get<T>(name, args);

        protected virtual void GetPage<T>(PageListDto<T> vm, IQueryable<T> query, int index, int size)
        {
            vm.Index = index < 1 ? 1 : index;
            vm.Size = size < 1 ? 10 : size;
            if (vm.Index > 1) query = query.Skip((vm.Index - 1) * vm.Size);

            vm.List = query.Take(vm.Size).ToList();
        }
    }
}
