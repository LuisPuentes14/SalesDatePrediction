using SalesDatePrediction.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Aplication.Interfaces
{
    public interface IGetAllShipperUseCase
    {
        Task<IEnumerable<ShipperDTO>> ExecuteAsync();
    }
}
