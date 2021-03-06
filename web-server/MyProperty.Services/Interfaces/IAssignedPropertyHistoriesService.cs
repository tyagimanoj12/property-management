using MyProperty.Data.Entities;
using MyProperty.Services.DTOs;
using MyProperty.Services.Helpers;
using System;
using System.Threading.Tasks;

namespace MyProperty.Services.Interfaces
{
    public interface IAssignedPropertyHistoriesService
    {
        Task<AssignedPropertyHistoryResponseDto> CreateAssignedPropertyHistory(AssignedPropertyHistoryForCreationDto request);
        Task<AssignedPropertyHistoryResponseDto> UpdateAssignedPropertyHistory(AssignedPropertyHistoryForUpdateDto request);
        Task<AssignedPropertyHistoryDto> GetAssignedPropertyHistory(Guid id);
        Task<PagedList<AssignedPropertyHistory>> GetAssignedPropertyHistories(AssignedPropertyHistoryResourceParametersDto assignedPropertyHistoryResourceParameters);
        Task<AssignedPropertyHistoryDeleteResponse> DeleteAssignedPropertyHistory(AssignedPropertyHistoryDeleteDto request);
    }
}
