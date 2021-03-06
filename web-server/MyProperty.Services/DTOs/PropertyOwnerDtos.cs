using System;

namespace MyProperty.Services.DTOs
{

    public abstract class BasePropertyOwnerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class PropertyOwnerForCreationDto : BasePropertyOwnerDto
    {

    }

    public class PropertyOwnerForUpdateDto : BasePropertyOwnerDto
    {
        public Guid PropertyOwnerId { get; set; }
    }

    public class PropertyOwnerResponseDto : BaseResponseDto
    {
        public Guid PropertyOwnerId { get; set; }
    }


    public class PropertyOwnerDto : BaseResponseDto
    {
        public Guid PropertyOwnerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class PropertyOwnerResourceParametersDto : BaseResourceParametersDto
    {
        public string OrderBy { get; set; } = "Name";
    }

    public class PropertyOwnerDeleteResponse : BaseResponseDto
    {
    }

    public class PropertyOwnerDeleteDto
    {
        public Guid PropertyOwnerId { get; set; }
    }
}

