﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Interfaces.Services
{
    public interface ISalesProxy
    {
        //Task<TOut> Get<TOut>(string uri);
        Task<string> Get(string paramsUri);
    }
}
