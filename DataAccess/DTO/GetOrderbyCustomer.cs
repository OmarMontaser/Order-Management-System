﻿using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class GetOrderbyCustomer
    {
        public ICollection<CreateOrder> Orders { get; set; }
    }
}
