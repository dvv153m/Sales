﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Interfaces.Services
{
    public interface IPromocodeService
    {
        Task<bool> AddPromocodeAsync();

        Task<bool> GetByPromocodeAsync(string promocode);        
    }
}
