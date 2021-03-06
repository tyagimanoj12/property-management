using MyProperty.Services.DTOs;
using System.Threading.Tasks;

namespace MyProperty.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDataDto> GetDashboardData();
    }
}
