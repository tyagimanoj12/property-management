using MyProperty.Data.Entities;
using MyProperty.Services.DTOs;
using MyProperty.Services.Helpers;
using System;
using System.Threading.Tasks;

namespace MyProperty.Services.Interfaces
{
    public interface IAssignedPropertiesService
    {
        Task<AssignedPropertyResponseDto> CreateAssignedProperty(AssignedPropertyForCreationDto request);
        Task<AssignedPropertyResponseDto> UpdateAssignedProperty(AssignedPropertyForUpdateDto request);
        Task<AssignedPropertyDto> GetAssignedProperty(Guid id);
        Task<PagedList<AssignedPropertyDataDto>> GetAssignedProperties(AssignedPropertyResourceParametersDto assignedPropertyResourceParameters);
        Task<AssignedPropertyDeleteResponse> DeleteAssignedProperty(AssignedPropertyDeleteDto request);
    }
}
