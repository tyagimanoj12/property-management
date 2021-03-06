using System;

namespace MyProperty.Services.DTOs
{
    public abstract class BaseAssignedPropertyHistoryDto
    {
        public Guid TenantId { get; set; }
        public Guid PropertyId { get; set; }
        public string Rent { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class AssignedPropertyHistoryForCreationDto : BaseAssignedPropertyHistoryDto
    {

    }

    public class AssignedPropertyHistoryForUpdateDto : BaseAssignedPropertyHistoryDto
    {
        public Guid AssignedPropertyHistoryId { get; set; }
    }

    public class AssignedPropertyHistoryResponseDto : BaseResponseDto
    {
        public Guid AssignedPropertyHistoryId { get; set; }
    }


    public class AssignedPropertyHistoryDto : BaseResponseDto
    {
        public Guid AssignedPropertyHistoryId { get; set; }
        public Guid TenantId { get; set; }
        public Guid PropertyId { get; set; }
        public string Rent { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class AssignedPropertyHistoryResourceParametersDto : BaseResourceParametersDto
    {
        public string OrderBy { get; set; } = "DateFrom";
    }

    public class AssignedPropertyHistoryDeleteResponse : BaseResponseDto
    {
    }

    public class AssignedPropertyHistoryDeleteDto
    {
        public Guid AssignedPropertyHistoryId { get; set; }
    }
}
