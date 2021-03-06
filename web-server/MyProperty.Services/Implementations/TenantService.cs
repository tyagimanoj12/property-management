using Microsoft.EntityFrameworkCore;
using MyProperty.Data;
using MyProperty.Data.Entities;
using MyProperty.Services.DTOs;
using MyProperty.Services.Helpers;
using MyProperty.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProperty.Services.Implementations
{
    public class TenantService : ITenantService
    {
        #region Fields

        private readonly MyPropertyContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IAuthContext _authContext;

        #endregion

        #region Constructor

        public TenantService(MyPropertyContext context, IPropertyMappingService propertyMappingService,
            IAuthContext authContext)
        {
            _context = context;
            _propertyMappingService = propertyMappingService;
            _authContext = authContext;
        }

        #endregion

        public async Task<TenantDto> GetTenant(Guid id)
        {
            var response = new TenantDto();
            try
            {
                var tenant = await _context.Tenants
                    .FirstOrDefaultAsync(x => x.TenantId == id);

                if (tenant == null)
                {
                    response.Message = "This tenant does not exist in the system.";
                    return response;
                }

                response.TenantId = tenant.TenantId;
                response.Name = tenant.Name;
                response.Address = tenant.Address;
                response.Phone = tenant.Phone;
                response.Email = tenant.Email;
                response.Success = true;
                response.Message = "";
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<PagedList<Tenant>> GetTenants(TenantResourceParametersDto tenantResourceParameters)
        {
            _propertyMappingService.InitPropertyMapping<TenantDto, Tenant>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "TenantId", new PropertyMappingValue(new List<string>(){ "TenantId" }) },
                { "Name", new PropertyMappingValue(new List<string>(){ "Name" }, true) },
                { "Address", new PropertyMappingValue(new List<string>(){ "Address" }) },
                { "Phone", new PropertyMappingValue(new List<string>(){ "Phone" }) },
                { "Email", new PropertyMappingValue(new List<string>(){ "Email" }, true) }
            });

            var collectionBeforePaging = _context.Tenants
                .Where(x => !x.Deleted)
                .ApplySort(tenantResourceParameters.OrderBy,
                _propertyMappingService.GetPropertyMapping<TenantDto, Tenant>());


            if (!string.IsNullOrEmpty(tenantResourceParameters.SearchQuery))
            {
                // trim and ignore casing
                var searchQueryForWhereClause = tenantResourceParameters.SearchQuery.TrimEnd().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.Name.ToLowerInvariant().Contains(searchQueryForWhereClause));
            }

            return PagedList<Tenant>.Create(collectionBeforePaging,
                tenantResourceParameters.PageNumber,
                tenantResourceParameters.PageSize);
        }

        public async Task<TenantResponseDto> CreateTenant(TenantForCreationDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var ownerId = _authContext.GetAuthOwnerId();

            var response = new TenantResponseDto();

            try
            {
                // preate tenant object for creation
                var tenant = new Tenant
                {
                    TenantId = Guid.NewGuid(),
                    Address = request.Address,
                    Phone = request.Phone,
                    Name = request.Name,
                    Email = request.Email,
                    CreatedBy = ownerId ?? Guid.Empty,
                    CreatedOn = DateTime.Now,
                };

                // add to context
                await _context.Tenants.AddAsync(tenant);
                // commit 
                _context.SaveChanges();

                // got so far 
                response.Success = true;
                response.TenantId = tenant.TenantId;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public async Task<TenantDeleteResponse> DeleteTenant(TenantDeleteDto request)
        {
            var response = new TenantDeleteResponse();
            try
            {
                var tenant = await _context.Tenants
                    .FirstOrDefaultAsync(x => x.TenantId == request.TenantId);

                if (tenant == null)
                {
                    response.Message = "This tenant does not exist in the system.";
                    return response;
                }

                tenant.Deleted = true;

                _context.SaveChanges();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<TenantResponseDto> UpdateTenant(TenantForUpdateDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = new TenantResponseDto();
            var ownerId = _authContext.GetAuthOwnerId();

            try
            {
                if (request.TenantId != Guid.Empty)
                {
                    var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.TenantId == request.TenantId);

                    // get tenant object for update
                    if (tenant != null)
                    {
                        tenant.Name = request.Name;
                        tenant.Address = request.Address;
                        tenant.Phone = request.Phone;
                        tenant.Email = request.Email;
                        tenant.UpdatedBy = ownerId ?? Guid.Empty;
                        tenant.UpdatedOn = DateTime.Now;
                    }

                    // commit 
                    _context.SaveChanges();
                    // got so far 
                    response.TenantId = tenant.TenantId;

                    response.Success = true;

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }
    }
}
