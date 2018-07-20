using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.Common;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.IService;


namespace AfxTcpFileServerSample.Service
{
    public class SyncInfoService : BaseService, ISyncInfoService
    {
        internal readonly static SyncInfoService Instance = new SyncInfoService();

        public virtual bool ReleaseLock()
        {
            try
            {
                var statusLockRepository = this.GetRepository<IStatusLockRepository>();
                return statusLockRepository.Release(StatusLockType.SyncInfo, "SyncInfo");
            }
            catch(Exception ex)
            {
                LogUtils.Error("FileInfoSyncService.ReleaseSyncLock", ex);
                return false;
            }
        }
    }
}
