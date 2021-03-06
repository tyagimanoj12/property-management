using MyProperty.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProperty.Web.Models
{
    public abstract class BaseTenantModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class TenantForCreationModel : BaseTenantModel
    {

    }

    public class TenantForUpdateModel : BaseTenantModel
    {
        public Guid TenantId { get; set; }
    }

    public class TenantModel
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class TenantResourceParametersModel : BaseResourceParametersModel
    {
        public string OrderBy { get; set; } = "Name";
    }

    public class TenantPagedResource
    {
        public List<TenantModel> Data { get; set; }
        public Page Page { get; set; }
    }
}
