using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyProperty.Data.Entities
{
    public class Property : BaseEntity
    {
        [Key]
        [Required]
        public Guid PropertyId { get; set; }

        [Required]
        public Guid PropertyOwnerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Rent { get; set; }

        [Required]
        public string Area { get; set; }

        public PropertyOwner PropertyOwner { get; set; }
        public virtual ICollection<AssignedProperty> AssignedProperties { get; set; }
        public virtual ICollection<AssignedPropertyHistory> AssignedPropertyHistories { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

    }
}
