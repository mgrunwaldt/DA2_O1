﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace Services
{
    public interface IAddressService
    {
        void AddAddress(Address a, User u);
    }
}
