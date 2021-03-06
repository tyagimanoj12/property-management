using System;

namespace MyProperty.Services.DTOs
{
    public abstract class BaseOwnerDto
    {
        public Guid OwnerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class OwnerPasswordValidateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class OwnerDto : BaseResponseDto
    {
        public Guid OwnerId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }

    public class UpdatePasswordDto
    {
        public Guid OwnerId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class UpdatePasswordResponse : BaseResponseDto
    {

    }
}
