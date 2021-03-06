using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyProperty.Services.DTOs;
using MyProperty.Services.Helpers;
using System;
using System.Threading.Tasks;

namespace MyProperty.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<PropertyResponseDto> CreateProperty(PropertyForCreationDto request);
        Task<PropertyResponseDto> UpdateProperty(PropertyForUpdateDto request);
        Task<PropertyDataDto> GetProperty(Guid id);
        Task<PagedList<Data.Entities.Property>> GetProperties(PropertyResourceParametersDto propertyResourceParameters);
        Task<PropertyDeleteResponse> DeleteProperty(PropertyDeleteDto request);
    }
}
