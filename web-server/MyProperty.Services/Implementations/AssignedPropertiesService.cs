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
    public class AssignedPropertiesService : IAssignedPropertiesService
    {
        #region Fields

        private readonly MyPropertyContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IAuthContext _authContext;

        #endregion

        #region Constructor

        public AssignedPropertiesService(MyPropertyContext context,
            IPropertyMappingService propertyMappingService, IAuthContext authContext)
        {
            _context = context;
            _propertyMappingService = propertyMappingService;
            _authContext = authContext;
        }

        #endregion

        #region Public Method(s)

        public async Task<AssignedPropertyDto> GetAssignedProperty(Guid id)
        {
            var response = new AssignedPropertyDto();
            try
            {
                var assignedProperty = await _context.AssignedProperties.Include(x => x.Tenant)
                    .FirstOrDefaultAsync(x => x.AssignedPropertyId == id);

                if (assignedProperty == null)
                {
                    response.Message = "This assignedProperty does not exist in the system.";
                    return response;
                }

                response.PropertyId = assignedProperty.PropertyId;
                response.Rent = assignedProperty.Rent;
                response.TenantId = assignedProperty.TenantId;
                response.AssignedPropertyId = assignedProperty.AssignedPropertyId;
                response.DateFrom = assignedProperty.DateFrom;
                response.DateTo = assignedProperty.DateTo;
                response.RentStartDate = assignedProperty.RentStartDate;
                
                response.Success = true;
                response.Message = "";
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<PagedList<AssignedPropertyDataDto>> GetAssignedProperties(AssignedPropertyResourceParametersDto assignedPropertyResourceParameters)
        {
            _propertyMappingService.InitPropertyMapping<AssignedPropertyDto, AssignedProperty>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "PropertyId", new PropertyMappingValue(new List<string>(){ "PropertyId" }) },
                { "TenantId", new PropertyMappingValue(new List<string>(){ "TenantId" }, true) },
                { "Rent", new PropertyMappingValue(new List<string>(){ "Rent" }, true) },
                { "DateFrom", new PropertyMappingValue(new List<string>(){ "DateFrom" }, true) },
                { "DateTo", new PropertyMappingValue(new List<string>(){ "DateTo" }, true) },
                { "RentStartDate", new PropertyMappingValue(new List<string>(){ "RentStartDate" }, true) },
                { "AssignedPropertyId", new PropertyMappingValue(new List<string>(){ "AssignedPropertyId" }) },
                { "PropertyOwnerId", new PropertyMappingValue(new List<string>(){ "PropertyOwnerId" }) },
                { "PropertyOwnerName", new PropertyMappingValue(new List<string>(){ "PropertyOwnerName" }) },
                { "PropertyName", new PropertyMappingValue(new List<string>(){ "PropertyName" }) },
                { "Address", new PropertyMappingValue(new List<string>(){ "Address" }) }
            });



            var collectionBeforePaging = _context.AssignedProperties
                .Include(x => x.Property).ThenInclude(z => z.PropertyOwner).Include(y => y.Tenant).Where(x => !x.Deleted)
                .ApplySort(assignedPropertyResourceParameters.OrderBy,
                _propertyMappingService.GetPropertyMapping<AssignedPropertyDto, AssignedProperty>());

            var query = _context.AssignedProperties.AsQueryable();           

            if (assignedPropertyResourceParameters.PropertyOwnerId != null)
            {
                query = query.Include(x => x.Property)
                        .ThenInclude(z => z.PropertyOwner).Include(y => y.Tenant)
                        .Where(x => x.Property.PropertyOwnerId == assignedPropertyResourceParameters.PropertyOwnerId && !x.Deleted)
                        .ApplySort(assignedPropertyResourceParameters.OrderBy,
                        _propertyMappingService.GetPropertyMapping<AssignedPropertyDto, AssignedProperty>());
            }
            else if (assignedPropertyResourceParameters.TenantId != null)
            {
                query = query.Include(y => y.Property)
                        .ThenInclude(z => z.PropertyOwner).Include(y => y.Tenant)
                        .Where(x => x.TenantId == assignedPropertyResourceParameters.TenantId && !x.Deleted)
                        .ApplySort(assignedPropertyResourceParameters.OrderBy,
                        _propertyMappingService.GetPropertyMapping<AssignedPropertyDto, AssignedProperty>());
            }        
            else
            {
                query = query.Include(y => y.Property)
                       .ThenInclude(z => z.PropertyOwner).Include(y => y.Tenant)
                       .Where(x => !x.Deleted)
                       .ApplySort(assignedPropertyResourceParameters.OrderBy,
                       _propertyMappingService.GetPropertyMapping<AssignedPropertyDto, AssignedProperty>());
            }

            var result = query.Select(x => new AssignedPropertyDataDto
            {
                Address = x.Property.Address,
                PropertyName = x.Property.Name,
                TenantName = x.Tenant.Name,
                AssignedPropertyId = x.AssignedPropertyId,
                PropertyId = x.PropertyId,
                TenantId = x.TenantId,
                Rent = x.Rent ?? x.Property.Rent,
                DateFrom = x.DateFrom,
                DateTo = x.DateTo,
                RentStartDate = x.RentStartDate,
                PropertyOwnerId = x.Property.PropertyOwner.PropertyOwnerId,
                PropertyOwnerName = x.Property.PropertyOwner.Name
            });

            if (!string.IsNullOrEmpty(assignedPropertyResourceParameters.SearchQuery))
            {
                // trim and ignore casing
                var searchQueryForWhereClause = assignedPropertyResourceParameters.SearchQuery.TrimEnd().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.Tenant.Name.ToLowerInvariant().Contains(searchQueryForWhereClause));
            }

            return PagedList<AssignedPropertyDataDto>.Create(result,
                assignedPropertyResourceParameters.PageNumber,
                assignedPropertyResourceParameters.PageSize);
        }

        public async Task<AssignedPropertyResponseDto> CreateAssignedProperty(AssignedPropertyForCreationDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = new AssignedPropertyResponseDto();

            var ownerId = _authContext.GetAuthOwnerId();

            try
            {
                var IsExistproperty = _context.AssignedProperties.Any(x => x.PropertyId == request.PropertyId && x.TenantId != request.TenantId && !x.Deleted);

                if (IsExistproperty)
                {
                    response.Message = "This property already assigned to another tenant. Please select another one";
                    response.Success = false;
                    return response;
                }

                var property = _context.Properties.FirstOrDefault(x => x.PropertyId == request.PropertyId);

                if (property != null)
                {
                    // create assignedProperty object for creation
                    var assignedProperty = new AssignedProperty
                    {
                        AssignedPropertyId = Guid.NewGuid(),
                        PropertyId = request.PropertyId,
                        TenantId = request.TenantId,
                        Rent = request.Rent ?? property.Rent,
                        DateFrom = request.DateFrom,
                        DateTo = request.DateTo,
                        RentStartDate = request.RentStartDate,
                        RentDocumentFilePath = request.RentDocumentFilePath,
                        CreatedBy = ownerId ?? Guid.Empty,
                        CreatedOn = DateTime.Now,
                    };
                    // add to context
                    await _context.AssignedProperties.AddAsync(assignedProperty);
                    // commit 
                    _context.SaveChanges();
                    // got so far 
                    response.Success = true;
                    response.AssignedPropertyId = assignedProperty.AssignedPropertyId;


                    // create assignedPropertyhistory object for creation
                    var assignedPropertyHistory = new AssignedPropertyHistory
                    {
                        AssignedPropertyHistoryId = Guid.NewGuid(),
                        PropertyId = request.PropertyId,
                        TenantId = request.TenantId,
                        Rent = request.Rent ?? property.Rent,
                        DateFrom = request.DateFrom,
                        DateTo = request.DateTo,
                        RentStartDate = request.RentStartDate,
                        RentDocumentFilePath =request.RentDocumentFilePath,                         
                        CreatedBy = ownerId ?? Guid.Empty,
                        CreatedOn = DateTime.Now,
                    };
                    // add to context
                    await _context.AssignedPropertyHistories.AddAsync(assignedPropertyHistory);
                    // commit 
                    _context.SaveChanges();
                    // got so far 
                    response.Success = true;
                    response.AssignedPropertyId = assignedProperty.AssignedPropertyId;





                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public async Task<AssignedPropertyResponseDto> UpdateAssignedProperty(AssignedPropertyForUpdateDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = new AssignedPropertyResponseDto();

            var ownerId = _authContext.GetAuthOwnerId();

            try
            {
                var IsExistproperty = _context.AssignedProperties.Any(x => x.PropertyId == request.PropertyId && x.TenantId != request.TenantId && !x.Deleted);

                if (IsExistproperty)
                {
                    response.Message = "This property already assigned to another tenant. Please select another one";
                    response.Success = false;
                    return response;
                }

                if (request.AssignedPropertyId != Guid.Empty)
                {
                    var assignedProperty = await _context.AssignedProperties.FirstOrDefaultAsync(x => x.AssignedPropertyId == request.AssignedPropertyId);

                    if (assignedProperty != null)
                    {
                        var property = _context.Properties.FirstOrDefault(x => x.PropertyId == request.PropertyId);

                        // get assignedProperty object for update
                        if (assignedProperty != null)
                        {
                            assignedProperty.TenantId = request.TenantId;
                            assignedProperty.Rent = request.Rent ?? property.Rent;
                            assignedProperty.PropertyId = request.PropertyId;
                            assignedProperty.DateFrom = request.DateFrom;
                            assignedProperty.DateTo = request.DateTo;
                            assignedProperty.RentStartDate = request.RentStartDate;
                            assignedProperty.UpdatedBy = ownerId;
                            assignedProperty.UpdatedOn = DateTime.Now;
                        }

                        // commit 
                        _context.SaveChanges();
                    }

                    // got so far 
                    response.AssignedPropertyId = assignedProperty.AssignedPropertyId;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<AssignedPropertyDeleteResponse> DeleteAssignedProperty(AssignedPropertyDeleteDto request)
        {
            var response = new AssignedPropertyDeleteResponse();
            try
            {
                var assignedProperty = await _context.AssignedProperties
                    .FirstOrDefaultAsync(x => x.AssignedPropertyId == request.AssignedPropertyId);

                if (assignedProperty == null)
                {
                    response.Message = "This assignedProperty does not exist in the system.";
                    return response;
                }

                assignedProperty.Deleted = true;

                _context.SaveChanges();

                response.Success = true;
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
