using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProperty.Services.DTOs;
using MyProperty.Services.Interfaces;
using MyProperty.Web.Models;
using System.Threading.Tasks;

namespace MyProperty.Web.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IOwnerService _ownerService;

        #endregion

        #region Constructor

        public TokenController(IMapper mapper, IOwnerService ownerService)
        {
            _mapper = mapper;
            _ownerService = ownerService;
        }

        #endregion

        #region Public Method(s)

        [HttpPost(Name = "Authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(OwnerModel), 200)]
        public async Task<IActionResult> Authenticate([FromBody]ValidatePasswordModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var request = _mapper.Map<OwnerPasswordValidateDto>(model);

            var ownerResponse = await _ownerService.Authenticate(request);

            if (!ownerResponse.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {ownerResponse.Message}");
            }

            var result = _mapper.Map<OwnerModel>(ownerResponse);

            return Ok(result);
        }

        [HttpPut("UpdatePassword")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody]UpdatePasswordModel model)
        {
            if (model == null) return BadRequest();

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var accountDto = _mapper.Map<UpdatePasswordDto>(model);

            var result = await _ownerService.UpdatePassword(accountDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        #endregion
    }
}
