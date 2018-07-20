using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Common
{
    public class MsgStatusException : Exception
    {
        public int Status { get; private set; }

        public MsgStatusException(int status, string msg): base(msg)
        {

        }
    }
}
