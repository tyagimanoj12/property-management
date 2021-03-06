using System;

namespace MyProperty.Services.DTOs
{
    public abstract class BasePropertyDto
    {
        public Guid PropertyOwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rent { get; set; }
        public string Area { get; set; }
    }

    public class PropertyForCreationDto : BasePropertyDto
    {

    }

    public class PropertyForUpdateDto : BasePropertyDto
    {
        public Guid PropertyId { get; set; }
    }

    public class PropertyResponseDto : BaseResponseDto
    {
        public Guid PropertyId { get; set; }
    }


    public class PropertyDto : BaseResponseDto
    {
        public Guid PropertyId { get; set; }
        public Guid PropertyOwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rent { get; set; }
        public string Area { get; set; }
    }

    public class PropertyResourceParametersDto : BaseResourceParametersDto
    {
        public string OrderBy { get; set; } = "Name";
    }

    public class PropertyDeleteResponse : BaseResponseDto
    {
    }

    public class PropertyDeleteDto
    {
        public Guid PropertyId { get; set; }
    }


    public class PropertyDataDto : BaseResponseDto
    {
        public Guid PropertyId { get; set; }
        public Guid PropertyOwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rent { get; set; }
        public string Area { get; set; }
        public string PropertyOwnerName { get; set; }
    }

}
