using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace MyProperty.Web.Models
{
    public abstract class BaseAssignedPropertyModel
    {
        public Guid TenantId { get; set; }
        public Guid PropertyId { get; set; }
        public string Rent { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime RentStartDate { get; set; }
        public string RentDocumentFilePath { get; set; }
    }

    public class AssignedPropertyForCreationModel : BaseAssignedPropertyModel
    {
       
    }

    public class AssignedPropertyForUpdateModel : BaseAssignedPropertyModel
    {
        public Guid AssignedPropertyId { get; set; }
    }

    public class AssignedPropertyModel
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

    public class AssignedPropertyResourceParametersModel : BaseResourceParametersModel
    {
        public string OrderBy { get; set; } = "Rent";
    }

    public class AssignedPropertyPagedResource
    {
        public List<AssignedPropertyModel> Data { get; set; }
        public Page Page { get; set; }
    }
}
