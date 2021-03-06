using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyProperty.Data;
using MyProperty.Services.DTOs;
using MyProperty.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyProperty.Services.Implementations
{
    public class OwnerService : IOwnerService
    {
        private readonly IMapper _mapper;
        private readonly MyPropertyContext _context;
        private readonly AppSettings _appSettings;

        public OwnerService(IMapper mapper,
            MyPropertyContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        public async Task<OwnerDto> Authenticate(OwnerPasswordValidateDto request)
        {
            var response = new OwnerDto();

            try
            {
                // get owner by emailAddress
                var owner = await _context.Owners.FirstOrDefaultAsync(x => x.UserName == request.Username && x.Password == request.Password);

                if (owner == null)
                {
                    response.Message = "Owner not found";
                    response.Success = false;
                    return response;
                }

                var isValidated = BCrypt.Net.BCrypt.Verify(request.Password, owner.PasswordHash);

                if (!isValidated)
                {
                    response.Message = "Owner not validated";
                    response.Success = false;
                    return response;
                };

                // got so far, it means this is authenticated user, 
                // let us create Authorization:  Bearer <Token> using jwt token 

                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                // token handler
                var tokenHandler = new JwtSecurityTokenHandler();

                // prepare security-token-descriptor object
                var expiresAt = DateTime.UtcNow.AddHours(6);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, owner.Email.ToString()),
                        new Claim(ClaimTypes.UserData, owner.OwnerId.ToString())
                    }),
                    Expires = expiresAt,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                // create and write token
                var token = tokenHandler.CreateToken(tokenDescriptor);

                response.Token = tokenHandler.WriteToken(token);
                response.ExpireAt = expiresAt;
                response.UserName = owner.UserName;
                response.Phone = owner.Phone;
                response.OwnerId = owner.OwnerId;
                response.Email = owner.Email;

                response.Success = true;
                response.Message = "Authenticate Successfully";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<UpdatePasswordResponse> UpdatePassword(UpdatePasswordDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = new UpdatePasswordResponse();

            try
            {
                var owner = _context.Owners
                    .FirstOrDefault(x => x.OwnerId == request.OwnerId && x.Password == request.OldPassword);

                if (owner == null)
                {
                    response.Message = "Owner not found";
                    response.Success = false;
                    return response;
                }

                // create hashed password
                var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

                owner.Password = request.NewPassword;
                owner.PasswordHash = newPasswordHash;

                await _context.SaveChangesAsync();

                response.Message = "Password update successfully";
                response.Success = true;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }
}

