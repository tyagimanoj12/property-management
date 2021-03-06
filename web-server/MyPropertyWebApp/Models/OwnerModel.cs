using System;

namespace MyProperty.Web.Models
{
    public class OwnerModel
    {
        public Guid OwnerId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
