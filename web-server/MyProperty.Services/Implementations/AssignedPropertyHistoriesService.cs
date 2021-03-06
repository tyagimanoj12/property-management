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
    public class AssignedPropertyHistoriesService : IAssignedPropertyHistoriesService
    {
        #region Fields

        private readonly MyPropertyContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IAuthContext _authContext;

        #endregion

        #region Constructor

        public AssignedPropertyHistoriesService(MyPropertyContext context,
            IPropertyMappingService propertyMappingService, IAuthContext authContext)
        {
            _context = context;
            _propertyMappingService = propertyMappingService;
            _authContext = authContext;
        }

        #endregion

        #region Public Method(s)

        public async Task<AssignedPropertyHistoryDto> GetAssignedPropertyHistory(Guid id)
        {
            var response = new AssignedPropertyHistoryDto();
            try
            {
                var assignedPropertyHistory = await _context.AssignedPropertyHistories
                    .Include(x => x.Property).Include(x => x.Tenant)
                    .FirstOrDefaultAsync(x => x.AssignedPropertyHistoryId == id);

                if (assignedPropertyHistory == null)
                {
                    response.Message = "This assignedPropertyHistory does not exist in the system.";
                    return response;
                }

                response.PropertyId = assignedPropertyHistory.PropertyId;
                response.Rent = assignedPropertyHistory.Rent;
                response.TenantId = assignedPropertyHistory.TenantId;
                response.AssignedPropertyHistoryId = assignedPropertyHistory.AssignedPropertyHistoryId;
                response.DateFrom = assignedPropertyHistory.DateFrom;
                response.DateTo = assignedPropertyHistory.DateTo;
                response.Success = true;
                response.Message = "";
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<PagedList<AssignedPropertyHistory>> GetAssignedPropertyHistories(AssignedPropertyHistoryResourceParametersDto assignedPropertyHistoryResourceParameters)
        {
            _propertyMappingService.InitPropertyMapping<AssignedPropertyHistoryDto, AssignedPropertyHistory>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "PropertyId", new PropertyMappingValue(new List<string>(){ "PropertyId" }) },
                { "TenantId", new PropertyMappingValue(new List<string>(){ "TenantId" }, true) },
                { "Rent", new PropertyMappingValue(new List<string>(){ "Rent" }, true) },
                { "AssignedPropertyHistoryId", new PropertyMappingValue(new List<string>(){ "AssignedPropertyHistoryId" }) },
                { "DateFrom", new PropertyMappingValue(new List<string>(){ "DateFrom" }) },
                { "DateTo", new PropertyMappingValue(new List<string>(){ "DateTo" }) },
            });

            var collectionBeforePaging = _context.AssignedPropertyHistories
                .Include(x => x.Property).Include(x => x.Tenant)
                .Where(x => !x.Deleted)
                .ApplySort(assignedPropertyHistoryResourceParameters.OrderBy,
                _propertyMappingService.GetPropertyMapping<AssignedPropertyHistoryDto, AssignedPropertyHistory>());


            if (!string.IsNullOrEmpty(assignedPropertyHistoryResourceParameters.SearchQuery))
            {
                // trim and ignore casing
                var searchQueryForWhereClause = assignedPropertyHistoryResourceParameters.SearchQuery.TrimEnd().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.Tenant.Name.ToLowerInvariant().Contains(searchQueryForWhereClause));
            }

            return PagedList<AssignedPropertyHistory>.Create(collectionBeforePaging,
                assignedPropertyHistoryResourceParameters.PageNumber,
                assignedPropertyHistoryResourceParameters.PageSize);
        }

        public async Task<AssignedPropertyHistoryResponseDto> CreateAssignedPropertyHistory(AssignedPropertyHistoryForCreationDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = new AssignedPropertyHistoryResponseDto();

            var ownerId = _authContext.GetAuthOwnerId();

            try
            {
                // create assignedPropertyHistory object for creation
                var assignedPropertyHistory = new AssignedPropertyHistory
                {
                    AssignedPropertyHistoryId = Guid.NewGuid(),
                    PropertyId = request.PropertyId,
                    TenantId = request.TenantId,
                    Rent = request.Rent,
                    DateFrom = request.DateFrom,
                    DateTo = request.DateTo,
                    CreatedBy = ownerId ?? Guid.Empty,
                    CreatedOn = DateTime.Now,
                };

                // add to context
                await _context.AssignedPropertyHistories.AddAsync(assignedPropertyHistory);
                // commit 
                _context.SaveChanges();

                // got so far 
                response.Success = true;
                response.AssignedPropertyHistoryId = assignedPropertyHistory.AssignedPropertyHistoryId;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public async Task<AssignedPropertyHistoryDeleteResponse> DeleteAssignedPropertyHistory(AssignedPropertyHistoryDeleteDto request)
        {
            var response = new AssignedPropertyHistoryDeleteResponse();
            try
            {
                var assignedPropertyHistory = await _context.AssignedPropertyHistories
                    .FirstOrDefaultAsync(x => x.AssignedPropertyHistoryId == request.AssignedPropertyHistoryId);

                if (assignedPropertyHistory == null)
                {
                    response.Message = "This assignedPropertyHistory does not exist in the system.";
                    return response;
                }

                assignedPropertyHistory.Deleted = true;

                _context.SaveChanges();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<AssignedPropertyHistoryResponseDto> UpdateAssignedPropertyHistory(AssignedPropertyHistoryForUpdateDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = new AssignedPropertyHistoryResponseDto();

            var ownerId = _authContext.GetAuthOwnerId();

            try
            {
                if (request.AssignedPropertyHistoryId != Guid.Empty)
                {
                    var assignedPropertyHistory = await _context.AssignedPropertyHistories.FirstOrDefaultAsync(x => x.TenantId == request.TenantId);

                    // get assignedPropertyHistory object for update
                    if (assignedPropertyHistory != null)
                    {
                        assignedPropertyHistory.TenantId = request.TenantId;
                        assignedPropertyHistory.Rent = request.Rent;
                        assignedPropertyHistory.PropertyId = request.PropertyId;
                        assignedPropertyHistory.DateTo = request.DateTo;
                        assignedPropertyHistory.DateFrom = request.DateFrom;
                        assignedPropertyHistory.UpdatedBy = ownerId;
                        assignedPropertyHistory.UpdatedOn = DateTime.Now;
                    }

                    // commit 
                    _context.SaveChanges();
                    // got so far 
                    response.AssignedPropertyHistoryId = assignedPropertyHistory.AssignedPropertyHistoryId;

                    response.Success = true;

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        #endregion
    }
}
