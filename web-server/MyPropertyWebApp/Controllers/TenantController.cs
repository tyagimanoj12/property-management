using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProperty.Services.DTOs;
using MyProperty.Services.Interfaces;
using MyProperty.Web.Models;
using System;
using System.Threading.Tasks;

namespace MyProperty.Web.Controllers
{
    [Route("api/tenants")]
    [ApiController]
    [Authorize]
    public class TenantController : ControllerBase
    {
        #region Fields

        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public TenantController(ITenantService tenantService,
           IMapper mapper)
        {
            _tenantService = tenantService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        [HttpGet(Name = "GetTenants")]
        [ProducesResponseType(typeof(TenantModel), 200)]
        public async Task<IActionResult> Get([FromQuery]TenantResourceParametersModel model)
        {
            var request = _mapper.Map<TenantResourceParametersDto>(model);
            var results = await _tenantService.GetTenants(request);
            var tenantsPagedResource = _mapper.Map<TenantPagedResource>(results);
            return Ok(tenantsPagedResource);
        }

        [HttpGet("{id}", Name = "GetTenant")]
        [ProducesResponseType(typeof(TenantModel), 200)]
        public async Task<IActionResult> GetTenant(Guid id)
        {
            var tenantResult = await _tenantService.GetTenant(id);

            if (!tenantResult.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {tenantResult.Message}");
            }

            var tenantModel = _mapper.Map<TenantModel>(tenantResult);

            return Ok(tenantModel);
        }

        [HttpPost(Name = "CreateTenant")]
        [ProducesResponseType(typeof(TenantModel), 200)]
        public async Task<IActionResult> Post([FromBody]TenantForCreationModel tenant)
        {
            if (tenant == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var tenantDto = _mapper.Map<TenantForCreationDto>(tenant);

            var result = await _tenantService.CreateTenant(tenantDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpPut(Name = "UpdateTenant")]
        [ProducesResponseType(typeof(TenantModel), 200)]
        public async Task<IActionResult> Put([FromBody]TenantForUpdateModel tenant)
        {
            if (tenant == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var tenantDto = _mapper.Map<TenantForUpdateDto>(tenant);

            var result = await _tenantService.UpdateTenant(tenantDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteTenant")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _tenantService.DeleteTenant(new TenantDeleteDto { TenantId = id });

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        #endregion
    }
}
