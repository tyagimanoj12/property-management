using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProperty.Data.Entities
{
    public class Tenant : BaseEntity
    {
        [Key]
        [Required]
        public Guid TenantId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual ICollection<AssignedProperty> AssignedProperties { get; set; }
        public virtual ICollection<AssignedPropertyHistory> AssignedPropertyHistories { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
