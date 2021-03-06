using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProperty.Web.Models
{
    public class ValidatePasswordModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UpdatePasswordModel
    {
        public Guid OwnerId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }        
    }
}
