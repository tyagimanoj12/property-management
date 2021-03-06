using MyProperty.Data.Entities;
using MyProperty.Services.DTOs;
using MyProperty.Services.Helpers;
using System;
using System.Threading.Tasks;

namespace MyProperty.Services.Interfaces
{
    public interface ITenantService
    {
        Task<TenantResponseDto> CreateTenant(TenantForCreationDto request);
        Task<TenantResponseDto> UpdateTenant(TenantForUpdateDto request);
        Task<TenantDto> GetTenant(Guid id);
        Task<PagedList<Tenant>> GetTenants(TenantResourceParametersDto TenantResourceParameters);
        Task<TenantDeleteResponse> DeleteTenant(TenantDeleteDto request);
    }
}
