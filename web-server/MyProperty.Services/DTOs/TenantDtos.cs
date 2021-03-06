using MyProperty.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProperty.Services.DTOs
{
    public abstract class BaseTenantDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class TenantForCreationDto : BaseTenantDto
    {

    }

    public class TenantForUpdateDto : BaseTenantDto
    {
        public Guid TenantId { get; set; }
    }

    public class TenantResponseDto : BaseResponseDto
    {
        public Guid TenantId { get; set; }
    }


    public class TenantDto : BaseResponseDto
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class TenantResourceParametersDto : BaseResourceParametersDto
    {
        public string OrderBy { get; set; } = "Name";
    }

    public class TenantDeleteResponse : BaseResponseDto
    {
    }

    public class TenantDeleteDto
    {
        public Guid TenantId { get; set; }
    }
}
