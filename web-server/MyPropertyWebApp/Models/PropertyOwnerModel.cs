using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProperty.Web.Models
{
    public abstract class BasePropertyOwnerModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class PropertyOwnerForCreationModel : BasePropertyOwnerModel
    {

    }

    public class PropertyOwnerForUpdateModel : BasePropertyOwnerModel
    {
        public Guid PropertyOwnerId { get; set; }
    }

    public class PropertyOwnerModel
    {
        public Guid PropertyOwnerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class PropertyOwnerResourceParametersModel : BaseResourceParametersModel
    {
        public string OrderBy { get; set; } = "Name";
    }

    public class PropertyOwnerPagedResource
    {
        public List<PropertyOwnerModel> Data { get; set; }
        public Page Page { get; set; }
    }
}
