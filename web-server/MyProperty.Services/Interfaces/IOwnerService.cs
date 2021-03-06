using MyProperty.Services.DTOs;
using System.Threading.Tasks;
using static MyProperty.Services.DTOs.OwnerDto;

namespace MyProperty.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<OwnerDto> Authenticate(OwnerPasswordValidateDto request);
        Task<UpdatePasswordResponse> UpdatePassword(UpdatePasswordDto request);
    }
}
