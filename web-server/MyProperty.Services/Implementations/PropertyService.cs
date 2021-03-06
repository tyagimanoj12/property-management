using Microsoft.EntityFrameworkCore;
using MyProperty.Data;
using MyProperty.Services.DTOs;
using MyProperty.Services.Helpers;
using MyProperty.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProperty.Services.Implementations
{
    public class PropertyService : IPropertyService
    {
        #region Fields

        private readonly MyPropertyContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IAuthContext _authContext;

        #endregion

        #region Constructor

        public PropertyService(MyPropertyContext context,
            IPropertyMappingService propertyMappingService,
            IAuthContext authContext)
        {
            _context = context;
            _propertyMappingService = propertyMappingService;
            _authContext = authContext;
        }

        #endregion

        #region Public Methods(s)

        public async Task<PropertyDataDto> GetProperty(Guid id)
        {
            var response = new PropertyDataDto();
            try
            {
                var property = await _context.Properties.Include(x => x.PropertyOwner)
                    .FirstOrDefaultAsync(x => x.PropertyId == id);

                if (property == null)
                {
                    response.Message = "This property does not exist in the system.";
                    return response;
                }

                response.PropertyId = property.PropertyId;
                response.Rent = property.Rent;
                response.Address = property.Address;
                response.Name = property.Name;
                response.PropertyOwnerId = property.PropertyOwnerId;
                response.Area = property.Area;
                response.PropertyOwnerName = property.PropertyOwner.Name;
                response.Success = true;
                response.Message = "";
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<PagedList<Data.Entities.Property>> GetProperties(PropertyResourceParametersDto propertyResourceParameters)
        {
            _propertyMappingService.InitPropertyMapping<PropertyDto, Data.Entities.Property>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "PropertyId", new PropertyMappingValue(new List<string>(){ "PropertyId" }) },
                { "PropertyOwnerId", new PropertyMappingValue(new List<string>(){ "PropertyOwnerId" }, true) },
                { "Name", new PropertyMappingValue(new List<string>(){ "Name" }, true) },
                { "Address", new PropertyMappingValue(new List<string>(){ "Address" }) },
                { "Area", new PropertyMappingValue(new List<string>(){ "Area" }) },
                { "Rent", new PropertyMappingValue(new List<string>(){ "Rent" }, true) }
            });

            var collectionBeforePaging = new List<Data.Entities.Property>().AsQueryable();

            collectionBeforePaging = propertyResourceParameters.PropertyOwnerId == null
                ? _context.Properties.Where(x => !x.Deleted)
               .ApplySort(propertyResourceParameters.OrderBy,
               _propertyMappingService.GetPropertyMapping<PropertyDto, Data.Entities.Property>())
                : _context.Properties
               .Where(x => x.PropertyOwnerId == propertyResourceParameters.PropertyOwnerId && !x.Deleted)
               .ApplySort(propertyResourceParameters.OrderBy,
               _propertyMappingService.GetPropertyMapping<PropertyDto, Data.Entities.Property>());

            if (!string.IsNullOrEmpty(propertyResourceParameters.SearchQuery))
            {
                // trim and ignore casing
                var searchQueryForWhereClause = propertyResourceParameters.SearchQuery.TrimEnd().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.Name.ToLowerInvariant().Contains(searchQueryForWhereClause));
            }

            return PagedList<Data.Entities.Property>.Create(collectionBeforePaging,
                propertyResourceParameters.PageNumber,
                propertyResourceParameters.PageSize);
        }

        public async Task<PropertyResponseDto> CreateProperty(PropertyForCreationDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = new PropertyResponseDto();

            var ownerId = _authContext.GetAuthOwnerId();

            try
            {
                // create property object for creation
                var property = new Data.Entities.Property
                {
                    PropertyId = Guid.NewGuid(),
                    Address = request.Address,
                    Area = request.Area,
                    Name = request.Name,
                    Rent = request.Rent,
                    PropertyOwnerId = request.PropertyOwnerId,
                    CreatedBy = ownerId ?? Guid.Empty,
                    CreatedOn = DateTime.Now,
                };

                // add to context
                await _context.Properties.AddAsync(property);
                // commit 
                _context.SaveChanges();

                // got so far 
                response.Success = true;
                response.PropertyId = property.PropertyId;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public async Task<PropertyResponseDto> UpdateProperty(PropertyForUpdateDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = new PropertyResponseDto();
            var ownerId = _authContext.GetAuthOwnerId();

            try
            {
                if (request.PropertyId != Guid.Empty)
                {
                    var property = await _context.Properties.FirstOrDefaultAsync(x => x.PropertyId == request.PropertyId);

                    // get propert object for update
                    if (property != null)
                    {
                        property.Name = request.Name;
                        property.Address = request.Address;
                        property.Area = request.Area;
                        property.PropertyOwnerId = request.PropertyOwnerId;
                        property.Rent = request.Rent;
                        property.UpdatedBy = ownerId ?? Guid.Empty;
                        property.UpdatedOn = DateTime.Now;
                    }

                    // commit 
                    _context.SaveChanges();
                    // got so far 
                    response.PropertyId = property.PropertyId;

                    response.Success = true;

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public async Task<PropertyDeleteResponse> DeleteProperty(PropertyDeleteDto request)
        {
            var response = new PropertyDeleteResponse();
            try
            {
                var property = await _context.Properties
                    .FirstOrDefaultAsync(x => x.PropertyId == request.PropertyId);

                if (property == null)
                {
                    response.Message = "This property does not exist in the system.";
                    return response;
                }

                property.Deleted = true;

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
