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
    [Route("api/assignedProperties")]
    [ApiController]
    [Authorize]
    public class AssignedPropertyController : ControllerBase
    {
        #region Fields

        private readonly IAssignedPropertiesService _assignedPropertiesService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AssignedPropertyController(IAssignedPropertiesService assignedPropertiesService,
           IMapper mapper)
        {
            _assignedPropertiesService = assignedPropertiesService;
            _mapper = mapper;
        }

        #endregion

        #region Public Method(s)

        [HttpGet(Name = "GetAssignedProperties")]
        [ProducesResponseType(typeof(AssignedPropertyModel), 200)]
        public async Task<IActionResult> Get([FromQuery]AssignedPropertyResourceParametersModel model)
        {
            var request = _mapper.Map<AssignedPropertyResourceParametersDto>(model);
            var results = await _assignedPropertiesService.GetAssignedProperties(request);
            var assignedPropertiesPagedResource = _mapper.Map<AssignedPropertyPagedResource>(results);
            return Ok(assignedPropertiesPagedResource);
        }

        [HttpGet("{id}", Name = "GetAssignedProperty")]
        [ProducesResponseType(typeof(AssignedPropertyModel), 200)]
        public async Task<IActionResult> GetAssignedProperty(Guid id)
        {
            var assignedPropertyResult = await _assignedPropertiesService.GetAssignedProperty(id);

            if (!assignedPropertyResult.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {assignedPropertyResult.Message}");
            }

            var assignedPropertyModel = _mapper.Map<AssignedPropertyModel>(assignedPropertyResult);

            return Ok(assignedPropertyModel);
        }

        [HttpPost(Name = "CreateAssignedProperty")]
        [ProducesResponseType(typeof(AssignedPropertyModel), 200)]
        public async Task<IActionResult> Post([FromBody]AssignedPropertyForCreationModel assignedProperty)
        {
            if (assignedProperty == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var assignedPropertyDto = _mapper.Map<AssignedPropertyForCreationDto>(assignedProperty);

            var result = await _assignedPropertiesService.CreateAssignedProperty(assignedPropertyDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpPut(Name = "UpdateAssignedProperty")]
        [ProducesResponseType(typeof(AssignedPropertyModel), 200)]
        public async Task<IActionResult> Put([FromBody]AssignedPropertyForUpdateModel assignedProperty)
        {
            if (assignedProperty == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var assignedPropertyDto = _mapper.Map<AssignedPropertyForUpdateDto>(assignedProperty);

            var result = await _assignedPropertiesService.UpdateAssignedProperty(assignedPropertyDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteAssignedProperty")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _assignedPropertiesService.DeleteAssignedProperty(new AssignedPropertyDeleteDto { AssignedPropertyId = id });

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        #endregion


    }
}
