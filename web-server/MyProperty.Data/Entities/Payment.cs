using System;
using System.ComponentModel.DataAnnotations;

namespace MyProperty.Data.Entities
{
    public class Payment : BaseEntity
    {
        [Key]
        [Required]
        public Guid PaymentId { get; set; }

        [Required]
        public Guid PropertyId { get; set; }

        public Guid? PropertyOwnerId { get; set; }

        public Guid? TenantId { get; set; }

        [Required]
        public string Amount { get; set; }
                
        public bool Credit { get; set; }

        public bool Debit { get; set; }

        public Property Property { get; set; }
        public PropertyOwner PropertyOwner { get; set; }
        public Tenant Tenant { get; set; }
    }
}
