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
    public class FileController : BaseController
    {
        [Auth(AuthType.WriteFile)]
        public ActionResult CreateFile()
        {
            CreateFileParamDto vm = this.GetData<CreateFileParamDto>();
            if(this.UserInfo.File.OpenType != FileOpenType.None)
            {
                return Failure("当前已打开其它文件，请先关闭其它文件！");
            }
            if(vm != null && !string.IsNullOrEmpty(vm.Name)
                && vm.CreationTime != DateTime.MinValue && vm.LastWriteTime != DateTime.MinValue)
            {
                FileDataParamDto fileDataParam = null;
                var fileService = this.GetService<IFileService>();
                if (fileService.CreateFile(vm, out fileDataParam))
                {
                    return Success(fileDataParam);
                }
            }

            return ParamError();
        }

        [Auth(AuthType.WriteFile)]
        public ActionResult UploadFileData()
        {
            FileDataDto vm = this.GetData<FileDataDto>();
            if(vm != null && this.UserInfo.File.OpenType == FileOpenType.Write)
            {
                FileDataParamDto fileDataParam = null;
                var fileService = this.GetService<IFileService>();
                if (fileService.UploadFileData(vm, out fileDataParam))
                {
                    return Success(fileDataParam);
                }
            }

            return ParamError();
        }

        [Auth(AuthType.WriteFile)]
        public ActionResult CreateFileCompleted()
        {
            if (this.UserInfo.File.OpenType != FileOpenType.Write)
            {
                return Failure("未打开创建文件！");
            }

            var fileService = this.GetService<IFileService>();
            bool result = fileService.CreateFileCompleted();

            return Success(result);
        }

        [Auth(AuthType.ReadFile)]
        public ActionResult OpenFile()
        {
            if (this.UserInfo.File.OpenType != FileOpenType.None)
            {
                return Failure("当前已打开其它文件，请先关闭其它文件！");
            }

            int id = this.GetData<int>();
            var fileService = this.GetService<IFileService>();
            bool result = fileService.OpenFile(id);

            return Success(result);
        }

        [Auth(AuthType.ReadFile)]
        public ActionResult GetFileData()
        {
            if (this.UserInfo.File.OpenType != FileOpenType.Read)
            {
                return Failure("未打开读取文件，请先打开文件！");
            }

            FileDataParamDto vm = this.GetData<FileDataParamDto>();
            if(vm != null)
            {
                var fileService = this.GetService<IFileService>();
                var m = fileService.GetFileData(vm);
                if(m != null)
                {
                    return Success(m);
                }
            }

            return ParamError();
        }

        public ActionResult CloseFile()
        {
            bool result = false;
            if (this.UserInfo.File.OpenType == FileOpenType.None)
            {
                var fileService = this.GetService<IFileService>();
                result = fileService.CloseFile();
            }

            return Success(result);
        }

        [Auth(AuthType.WriteFile)]
        public ActionResult CreateDirectory()
        {
            CreateDirectoryParamDto vm = this.GetData<CreateDirectoryParamDto>();
            if(vm != null && !string.IsNullOrEmpty(vm.Name)
                && vm.CreationTime != DateTime.MinValue && vm.LastWriteTime != DateTime.MinValue)
            {
                var fileService = this.GetService<IFileService>();
                bool result = fileService.CreateDirectory(vm);

                return Success(result);
            }

            return ParamError();
        }

        [Auth(AuthType.WriteFile)]
        public ActionResult DeleteOpenFile()
        {
            var fileService = this.GetService<IFileService>();
            bool result = fileService.DeleteOpenFile();

            return Success(result);
        }

        [Auth(new AuthType[] { AuthType.ReadFile, AuthType.WriteFile })]
        public ActionResult GetPhysicaPath()
        {
            string path = this.GetData<string>();
            var fileService = this.GetService<IFileService>();
            var result = fileService.GetPhysicaPath(path);

            return Success(result);
        }

        [Auth(AuthType.WriteFile)]
        public ActionResult AddFileInfo()
        {
            AddFileInfoDto vm = this.GetData<AddFileInfoDto>();
            if(vm != null)
            {
                var fileService = this.GetService<IFileService>();
                bool result = fileService.AddFileInfo(vm);

                return Success(result);
            }

            return ParamError();
        }
    }
}
