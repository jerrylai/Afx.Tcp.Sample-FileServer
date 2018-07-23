using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.IService;
using AfxTcpFileServerSample.Enums;
using Afx.Tcp.Host;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.Controllers
{
    public class FileInfoController : BaseController
    {
        [Auth(AuthType.ReadFile)]
        public ActionResult Get(int id)
        {
            if(id > 0)
            {
                var fileInfoService = this.GetService<IFileInfoService>();
                var m = fileInfoService.Get(id);

                return Success(m);
            }

            return Error();
        }

        [Auth(new AuthType[] { AuthType.ReadFile, AuthType.WriteFile })]
        public ActionResult Exist(FileInfoParamDto vm)
        {
            if (vm != null && !string.IsNullOrEmpty(vm.Name) && vm.Type != (int)FileInfoType.None)
            {
                var fileInfoService = this.GetService<IFileInfoService>();
                bool result = fileInfoService.Exist(vm);

                return Success(result);
            }

            return Error();
        }


        [Auth(AuthType.WriteFile)]
        public ActionResult Delete(int id)
        {
            var fileInfoService = this.GetService<IFileInfoService>();
            bool result = fileInfoService.Delete(id);

            return Success(result);
        }

        [Auth(AuthType.ReadFile)]
        public ActionResult GetPageList(FileInfoPageParamDto vm)
        {
            if(vm != null && vm.Index > 0 && vm.Size > 0)
            {
                var fileInfoService = this.GetService<IFileInfoService>();
                PageListDto<FileInfoDto> data = fileInfoService.GetPageList(vm);

                return Success(data);
            }

            return Error();
        }

        [Auth(AuthType.System)]
        public ActionResult GetSysList(SyncParamDto vm)
        {
            if (vm != null && vm.Count > 0)
            {
                var fileInfoService = this.GetService<IFileInfoService>();
                List<FileInfoDto> data = fileInfoService.GetSysList(vm);

                return Success(data);
            }

            return Error();
        }

        [Auth(AuthType.System)]
        public ActionResult RestCheck()
        {
            var fileInfoService = this.GetService<IFileInfoService>();
            if (fileInfoService.RestCheckStatus())
            {
                fileInfoService.CheckFileInfo();
            }

            return Success(true);
        }
    }
}
