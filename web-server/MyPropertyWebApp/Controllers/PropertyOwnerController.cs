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
    [Route("api/propertyOwners")]
    [ApiController]
    [Authorize]
    public class PropertyOwnerController : ControllerBase
    {
        #region Fields

        private readonly IPropertyOwnerService _propertyOwnerService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public PropertyOwnerController(IPropertyOwnerService propertyOwnerService,
           IMapper mapper)
        {
            _propertyOwnerService = propertyOwnerService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        [HttpGet(Name = "GetPropertyOwners")]
        [ProducesResponseType(typeof(PropertyOwnerModel), 200)]
        public async Task<IActionResult> Get([FromQuery]PropertyOwnerResourceParametersModel model)
        {
            var request = _mapper.Map<PropertyOwnerResourceParametersDto>(model);
            var results = await _propertyOwnerService.GetPropertyOwners(request);
            var propertyOwnersPagedResource = _mapper.Map<PropertyOwnerPagedResource>(results);
            return Ok(propertyOwnersPagedResource);
        }

        [HttpGet("{id}", Name = "GetPropertyOwner")]
        [ProducesResponseType(typeof(PropertyOwnerModel), 200)]
        public async Task<IActionResult> GetPropertyOwner(Guid id)
        {
            var propertyOwnerResult = await _propertyOwnerService.GetPropertyOwner(id);

            if (!propertyOwnerResult.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {propertyOwnerResult.Message}");
            }

            var propertyOwnerModel = _mapper.Map<PropertyOwnerModel>(propertyOwnerResult);

            return Ok(propertyOwnerModel);
        }

        [HttpPost(Name = "CreatePropertyOwner")]
        [ProducesResponseType(typeof(PropertyOwnerModel), 200)]
        public async Task<IActionResult> Post([FromBody]PropertyOwnerForCreationModel propertyOwner)
        {
            if (propertyOwner == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var propertyOwnerDto = _mapper.Map<PropertyOwnerForCreationDto>(propertyOwner);

            var result = await _propertyOwnerService.CreatePropertyOwner(propertyOwnerDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpPut(Name = "UpdatePropertyOwner")]
        [ProducesResponseType(typeof(PropertyOwnerModel), 200)]
        public async Task<IActionResult> Put([FromBody]PropertyOwnerForUpdateModel propertyOwner)
        {
            if (propertyOwner == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var propertyOwnerDto = _mapper.Map<PropertyOwnerForUpdateDto>(propertyOwner);

            var result = await _propertyOwnerService.UpdatePropertyOwner(propertyOwnerDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeletePropertyOwner")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _propertyOwnerService.DeletePropertyOwner(new PropertyOwnerDeleteDto { PropertyOwnerId = id });

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        #endregion
    }
}
