﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.IService
{
    public interface IBaseService: IDisposable
    {
        bool IsDisposed { get; }
    }
}