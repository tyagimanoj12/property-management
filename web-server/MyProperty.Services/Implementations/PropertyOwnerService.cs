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
    public class PropertyOwnerService : IPropertyOwnerService
    {
        #region Fields

        private readonly MyPropertyContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IAuthContext _authContext;

        #endregion

        #region Constructor

        public PropertyOwnerService(MyPropertyContext context, IPropertyMappingService propertyMappingService,
            IAuthContext authContext)
        {
            _context = context;
            _propertyMappingService = propertyMappingService;
            _authContext = authContext;
        }

        #endregion

        #region Public Method(s)

        public async Task<PropertyOwnerResponseDto> CreatePropertyOwner(PropertyOwnerForCreationDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var ownerId = _authContext.GetAuthOwnerId();

            var response = new PropertyOwnerResponseDto();

            try
            {
                // create property owner object for creation
                var propertyOwner = new PropertyOwner
                {
                    PropertyOwnerId = Guid.NewGuid(),
                    Address = request.Address,
                    Phone = request.Phone,
                    Name = request.Name,
                    Email = request.Email,
                    CreatedBy = ownerId ?? Guid.Empty,
                    CreatedOn = DateTime.Now,
                };

                // add to context
                await _context.PropertyOwners.AddAsync(propertyOwner);
                // commit 
                _context.SaveChanges();

                // got so far 
                response.Success = true;
                response.PropertyOwnerId = propertyOwner.PropertyOwnerId;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public async Task<PropertyOwnerDeleteResponse> DeletePropertyOwner(PropertyOwnerDeleteDto request)
        {
            var response = new PropertyOwnerDeleteResponse();
            try
            {
                var propertyOwner = await _context.PropertyOwners
                    .FirstOrDefaultAsync(x => x.PropertyOwnerId == request.PropertyOwnerId);

                if (propertyOwner == null)
                {
                    response.Message = "This PropertyOwner does not exist in the system.";
                    return response;
                }

                propertyOwner.Deleted = true;

                _context.SaveChanges();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<PropertyOwnerDto> GetPropertyOwner(Guid id)
        {
            var response = new PropertyOwnerDto();
            try
            {
                var propertyOwner = await _context.PropertyOwners
                    .FirstOrDefaultAsync(x => x.PropertyOwnerId == id);

                if (propertyOwner == null)
                {
                    response.Message = "This propertyOwner does not exist in the system.";
                    return response;
                }

                response.PropertyOwnerId = propertyOwner.PropertyOwnerId;
                response.Name = propertyOwner.Name;
                response.Address = propertyOwner.Address;
                response.Phone = propertyOwner.Phone;
                response.Email = propertyOwner.Email;
                response.Success = true;
                response.Message = "";
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<PagedList<PropertyOwner>> GetPropertyOwners(PropertyOwnerResourceParametersDto propertyOwnerResourceParameters)
        {
            _propertyMappingService.InitPropertyMapping<PropertyOwnerDto, PropertyOwner>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "PropertyOwnerId", new PropertyMappingValue(new List<string>(){ "PropertyOwnerId" }) },
                { "Name", new PropertyMappingValue(new List<string>(){ "Name" }, true) },
                { "Address", new PropertyMappingValue(new List<string>(){ "Address" }) },
                { "Phone", new PropertyMappingValue(new List<string>(){ "Phone" }) },
                { "Email", new PropertyMappingValue(new List<string>(){ "Email" }, true) }
            });

            var collectionBeforePaging = _context.PropertyOwners
                .Where(x => !x.Deleted)
                .ApplySort(propertyOwnerResourceParameters.OrderBy,
                _propertyMappingService.GetPropertyMapping<PropertyOwnerDto, PropertyOwner>());


            if (!string.IsNullOrEmpty(propertyOwnerResourceParameters.SearchQuery))
            {
                // trim and ignore casing
                var searchQueryForWhereClause = propertyOwnerResourceParameters.SearchQuery.TrimEnd().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.Name.ToLowerInvariant().Contains(searchQueryForWhereClause));
            }

            return PagedList<PropertyOwner>.Create(collectionBeforePaging,
                propertyOwnerResourceParameters.PageNumber,
                propertyOwnerResourceParameters.PageSize);
        }

        public async Task<PropertyOwnerResponseDto> UpdatePropertyOwner(PropertyOwnerForUpdateDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = new PropertyOwnerResponseDto();
            var ownerId = _authContext.GetAuthOwnerId();

            try
            {
                if (request.PropertyOwnerId != Guid.Empty)
                {
                    var propertyOwner = await _context.PropertyOwners.FirstOrDefaultAsync(x => x.PropertyOwnerId == request.PropertyOwnerId);

                    // get PropertyOwner object for update
                    if (propertyOwner != null)
                    {
                        propertyOwner.Name = request.Name;
                        propertyOwner.Address = request.Address;
                        propertyOwner.Phone = request.Phone;
                        propertyOwner.Email = request.Email;
                        propertyOwner.UpdatedBy = ownerId ?? Guid.Empty;
                        propertyOwner.UpdatedOn = DateTime.Now;
                    }

                    // commit 
                    _context.SaveChanges();
                    // got so far 
                    response.PropertyOwnerId = propertyOwner.PropertyOwnerId;

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
