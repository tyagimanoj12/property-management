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
    [Route("api/properties")]
    [ApiController]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        #region Fields

        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public PropertyController(IPropertyService propertyService,
           IMapper mapper)
        {
            _propertyService = propertyService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        [HttpGet(Name = "GetProperties")]
        [ProducesResponseType(typeof(PropertyModel), 200)]
        public async Task<IActionResult> Get([FromQuery]PropertyResourceParametersModel model)
        {
            var request = _mapper.Map<PropertyResourceParametersDto>(model);
            var results = await _propertyService.GetProperties(request);
            var propertiesPagedResource = _mapper.Map<PropertyPagedResource>(results);
            return Ok(propertiesPagedResource);
        }

        [HttpGet("{id}", Name = "GetProperty")]
        [ProducesResponseType(typeof(PropertyModel), 200)]
        public async Task<IActionResult> GetProperty(Guid id)
        {
            var propertyResult = await _propertyService.GetProperty(id);

            if (!propertyResult.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {propertyResult.Message}");
            }

            var propertyModel = _mapper.Map<PropertyModel>(propertyResult);

            return Ok(propertyModel);
        }

        [HttpPost(Name = "CreateProperty")]
        [ProducesResponseType(typeof(PropertyModel), 200)]
        public async Task<IActionResult> Post([FromBody]PropertyForCreationModel property)
        {
            if (property == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var propertyDto = _mapper.Map<PropertyForCreationDto>(property);

            var result = await _propertyService.CreateProperty(propertyDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpPut(Name = "UpdateProperty")]
        [ProducesResponseType(typeof(PropertyModel), 200)]
        public async Task<IActionResult> Put([FromBody]PropertyForUpdateModel property)
        {
            if (property == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var propertyDto = _mapper.Map<PropertyForUpdateDto>(property);

            var result = await _propertyService.UpdateProperty(propertyDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteProperty")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _propertyService.DeleteProperty(new PropertyDeleteDto { PropertyId = id });

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        #endregion

    }
}
