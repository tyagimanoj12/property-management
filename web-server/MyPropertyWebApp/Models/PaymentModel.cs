using System;
using System.Collections.Generic;

namespace MyProperty.Web.Models
{
    public abstract class BasePaymentModel
    {
        public Guid PropertyId { get; set; }
        public Guid? PropertyOwnerId { get; set; }
        public Guid? TenantId { get; set; }
        public string Amount { get; set; }
        public bool Credit { get; set; }
        public bool Debit { get; set; }
    }

    public class PaymentForCreationModel : BasePaymentModel
    {

    }

    public class PaymentForUpdateModel : BasePaymentModel
    {
        public Guid PaymentId { get; set; }
    }

    public class PaymentModel
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

    public class PaymentResourceParametersModel : BaseResourceParametersModel
    {
        public string OrderBy { get; set; } = "Credit";
    }

    public class PaymentPagedResource
    {
        public List<PaymentModel> Data { get; set; }
        public Page Page { get; set; }
    }
}
