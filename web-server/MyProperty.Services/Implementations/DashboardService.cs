using Microsoft.EntityFrameworkCore;
using MyProperty.Data;
using MyProperty.Services.DTOs;
using MyProperty.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyProperty.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        #region Fields

        private readonly MyPropertyContext _context;
        private readonly IAuthContext _authContext;

        #endregion

        #region Constructor

        public DashboardService(MyPropertyContext context,
            IAuthContext authContext)
        {
            _context = context;
            _authContext = authContext;
        }

        #endregion

        #region Public Method(s)

        public async Task<DashboardDataDto> GetDashboardData()
        {
            var response = new DashboardDataDto();
            try
            {
                var properties = await _context.Properties.Where(x => x.Deleted == false).ToListAsync();

                if (properties == null)
                {
                    response.Message = "Properties does not exist in the system.";
                    return response;
                }

                var assignedProperties = await _context.AssignedProperties.Where(x => x.Deleted == false).ToListAsync();

                if (assignedProperties == null)
                {
                    response.Message = "AssignedProperties does not exist in the system.";
                    return response;
                }

                var payments = await _context.Payments.Where(x => x.Deleted == false).ToListAsync();

                if (payments == null)
                {
                    response.Message = "Payments does not exist in the system.";
                    return response;
                }

                response.TotalProperties = properties.Count;
                response.AssignedProperties = assignedProperties.Count;
                response.VacantProperties = properties.Count - assignedProperties.Count;
                response.TotalPayments = payments.Sum(x => double.Parse(x.Amount));
                response.CreditPayments = payments.Where(x => x.Credit).Sum(x => double.Parse(x.Amount));
                response.DebitPayments = payments.Where(x => x.Debit).Sum(x => double.Parse(x.Amount));
                response.Success = true;
                response.Message = "";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }

            return response;
        }


        #endregion
    }
}

