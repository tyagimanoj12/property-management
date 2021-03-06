using MyProperty.Data.Entities;
using MyProperty.Services.DTOs;
using MyProperty.Services.Helpers;
using System;
using System.Threading.Tasks;

namespace MyProperty.Services.Interfaces
{
    public interface IPropertyOwnerService
    {
        Task<PropertyOwnerResponseDto> CreatePropertyOwner(PropertyOwnerForCreationDto request);
        Task<PropertyOwnerResponseDto> UpdatePropertyOwner(PropertyOwnerForUpdateDto request);
        Task<PropertyOwnerDto> GetPropertyOwner(Guid id);
        Task<PagedList<PropertyOwner>> GetPropertyOwners(PropertyOwnerResourceParametersDto propertyOwnerResourceParameters);
        Task<PropertyOwnerDeleteResponse> DeletePropertyOwner(PropertyOwnerDeleteDto request);
    }
}
