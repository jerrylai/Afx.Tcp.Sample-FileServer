using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using AfxTcpFileServerSample.Enums;


namespace AfxTcpFileServerSample.Common
{
    public class UserFile : IDisposable
    {
        public FileOpenType OpenType { get; set; }

        public string Directory { get; set; }

        public string Name { get; set; }

        public long Length { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime LastWriteTime { get; set; }

        public long Position { get; set; }

        public string TempName { get; set; }

        public FileStream Stream { get; set; }

        public UserFile()
        {
            this.Rest();
        }

        public void Rest()
        {
            this.OpenType = FileOpenType.None;
            this.Directory = "";
            this.Name = "";
            this.Length = 0;
            this.CreationTime = DateTime.MinValue;
            this.LastWriteTime = DateTime.MinValue;
            this.Position = 0;
            this.TempName = "";
            if (this.Stream != null)
            {
                try{ this.Stream.Close(); }
                catch { }
            }
            this.Stream = null;
        }

        public void Dispose()
        {
            this.Rest();
        }
    }
}
