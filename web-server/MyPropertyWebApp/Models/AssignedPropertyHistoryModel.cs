using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProperty.Web.Models
{
    public abstract class BaseAssignedPropertyHistoryModel
    {
        public Guid TenantId { get; set; }
        public Guid PropertyId { get; set; }
        public string Rent { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class AssignedPropertyHistoryForCreationModel : BaseAssignedPropertyHistoryModel
    {

    }

    public class AssignedPropertyHistoryForUpdateModel : BaseAssignedPropertyHistoryModel
    {
        public Guid AssignedPropertyHistoryId { get; set; }
    }

    public class AssignedPropertyHistoryModel
    {
        public Guid AssignedPropertyHistoryId { get; set; }
        public Guid TenantId { get; set; }
        public Guid PropertyId { get; set; }
        public string Rent { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class AssignedPropertyHistoryResourceParametersModel : BaseResourceParametersModel
    {
        public string OrderBy { get; set; } = "DateFrom";
    }

    public class AssignedPropertyHistoryPagedResource
    {
        public List<AssignedPropertyHistoryModel> Data { get; set; }
        public Page Page { get; set; }
    }
}
