using System;

namespace MyProperty.Services.Interfaces
{
    public interface IAuthContext
    {
        Guid? GetAuthOwnerId();
        string GetAuthUsername();
    }
}
