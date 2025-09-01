﻿using SalesDatePrediction.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
    }
}
