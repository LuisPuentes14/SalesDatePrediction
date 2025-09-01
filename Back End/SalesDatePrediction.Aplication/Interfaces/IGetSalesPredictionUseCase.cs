using SalesDatePrediction.Aplication.DTOs;
using SalesDatePrediction.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Aplication.Interfaces
{
    public interface IGetSalesPredictionUseCase
    {
        Task<IEnumerable<SalesDatePredictionDTO>> ExecuteAsync();
    }
}
