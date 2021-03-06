using System;
using System.Collections.Generic;

namespace MyProperty.Web.Models
{
    public abstract class BasePropertyModel
    {
        public Guid PropertyOwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rent { get; set; }
        public string Area { get; set; }
    }

    public class PropertyForCreationModel : BasePropertyModel
    {

    }

    public class PropertyForUpdateModel : BasePropertyModel
    {
        public Guid PropertyId { get; set; }
    }

    public class PropertyModel
    {
        public Guid PropertyId { get; set; }
        public Guid PropertyOwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rent { get; set; }
        public string Area { get; set; }
        public string PropertyOwnerName { get; set; }
    }

    public class PropertyResourceParametersModel : BaseResourceParametersModel
    {
        public string OrderBy { get; set; } = "Name";
    }

    public class PropertyPagedResource
    {
        public List<PropertyModel> Data { get; set; }
        public Page Page { get; set; }
    }

}
