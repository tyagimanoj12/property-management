using Microsoft.AspNetCore.Http;
using MyProperty.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyProperty.Services.Implementations
{
    public class AuthContext : IAuthContext
    {

        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuthContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public Guid? GetAuthOwnerId()
        {
            if (_httpContextAccessor.HttpContext?.User == null)
            {
                return (Guid?)null;
            }

            var nameIdentifierClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
            return nameIdentifierClaim?.Value != null ? new Guid(nameIdentifierClaim.Value) : (Guid?)null ;
        }

        public string GetAuthUsername()
        {
            if (_httpContextAccessor.HttpContext?.User == null)
            {
                return "";
            }

            var nameClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            return nameClaim?.Value ?? "";
        }

        
    }
}
