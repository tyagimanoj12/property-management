using System;
using System.ComponentModel.DataAnnotations;

namespace MyProperty.Data.Entities
{
    public class AssignedProperty : BaseEntity
    {
        [Key]
        [Required]
        public Guid AssignedPropertyId { get; set; }

        [Required]
        public Guid TenantId { get; set; }

        [Required]
        public Guid PropertyId { get; set; }

        [Required]
        public string Rent { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public DateTime RentStartDate { get; set; }

        public string RentDocumentFilePath { get; set; }

        public Tenant Tenant { get; set; }

        public Property Property { get; set; }

    }
}
