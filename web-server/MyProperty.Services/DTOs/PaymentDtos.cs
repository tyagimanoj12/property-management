using System;

namespace MyProperty.Services.DTOs
{
    public abstract class BasePaymentDto
    {
        public Guid PropertyId { get; set; }
        public Guid? PropertyOwnerId { get; set; }
        public Guid? TenantId { get; set; }
        public string Amount { get; set; }
        public bool Credit { get; set; }
        public bool Debit { get; set; }
    }

    public class PaymentForCreationDto : BasePaymentDto
    {

    }

    public class PaymentForUpdateDto : BasePaymentDto
    {
        public Guid PaymentId { get; set; }
    }

    public class PaymentResponseDto : BaseResponseDto
    {
        public Guid PaymentId { get; set; }
    }


    public class PaymentDto : BaseResponseDto
    {
        public Guid PaymentId { get; set; }
        public Guid PropertyId { get; set; }
        public Guid? PropertyOwnerId { get; set; }
        public Guid? TenantId { get; set; }
        public string Amount { get; set; }
        public bool Credit { get; set; }
        public bool Debit { get; set; }
        public int PaymentType { get; set; }
    }

    public class PaymentResourceParametersDto : BaseResourceParametersDto
    {
        public string OrderBy { get; set; } = "Credit";
    }

    public class PaymentDeleteResponse : BaseResponseDto
    {
    }

    public class PaymentDeleteDto
    {
        public Guid PaymentId { get; set; }
    }

    public class PaymentDataDto 
    {
        public Guid PaymentId { get; set; }
        public Guid PropertyId { get; set; }
        public Guid? PropertyOwnerId { get; set; }
        public Guid? TenantId { get; set; }
        public string Amount { get; set; }
        public bool Credit { get; set; }
        public bool Debit { get; set; }
        public string PropertyName { get; set; }
        public string PropertyOwnerName { get; set; }
        public string TenantName { get; set; }
        public string PaymentType { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
