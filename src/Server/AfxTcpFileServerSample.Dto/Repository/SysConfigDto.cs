using AfxTcpFileServerSample.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.Dto.Repository
{
    public class SysConfigDto
    {
        public int Id { get; set; }

        public ConfigType Type { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
