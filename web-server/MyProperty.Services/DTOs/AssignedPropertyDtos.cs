using System;

namespace MyProperty.Services.DTOs
{
    public abstract class BaseAssignedPropertyDto
    {
        public Guid TenantId { get; set; }
        public Guid PropertyId { get; set; }
        public string Rent { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime RentStartDate { get; set; }
        public string RentDocumentFilePath { get; set; }
    }

    public class AssignedPropertyForCreationDto : BaseAssignedPropertyDto
    {

    }

    public class AssignedPropertyForUpdateDto : BaseAssignedPropertyDto
    {
        public Guid AssignedPropertyId { get; set; }
    }

    public class AssignedPropertyResponseDto : BaseResponseDto
    {
        public Guid AssignedPropertyId { get; set; }
    }


    public class AssignedPropertyDto : BaseResponseDto
    {
        public Guid AssignedPropertyId { get; set; }
        public Guid TenantId { get; set; }
        public Guid PropertyId { get; set; }
        public string Rent { get; set; }        
        public string TenantName { get; set; }
        public string PropertyName { get; set; }
        public string Address { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime RentStartDate { get; set; }
        public Guid PropertyOwnerId { get; set; }
        public string PropertyOwnerName { get; set; }
    }

    public class AssignedPropertyResourceParametersDto : BaseResourceParametersDto
    {
        public string OrderBy { get; set; } = "Rent";
    }

    public class AssignedPropertyDeleteResponse : BaseResponseDto
    {
    }

    public class AssignedPropertyDeleteDto
    {
        public Guid AssignedPropertyId { get; set; }
    }

    public class AssignedPropertyDataDto
    {
        public Guid AssignedPropertyId { get; set; }
        public Guid TenantId { get; set; }
        public Guid PropertyId { get; set; }
        public string Rent { get; set; }
        public string TenantName { get; set; }
        public string PropertyName { get; set; }
        public string Address { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime RentStartDate { get; set; }
        public Guid PropertyOwnerId { get; set; }
        public string PropertyOwnerName { get; set; }
    }
}
