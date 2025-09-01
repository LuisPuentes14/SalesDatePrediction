using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<SalesDatePredictions>> GetSalesDatePrediction();
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(long id);
      
    }
}
