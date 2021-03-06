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
    [Route("api/assignedPropertyHistories")]
    [ApiController]
    [Authorize]
    public class AssignedPropertyHistoryController : ControllerBase
    {
        #region Fields

        private readonly IAssignedPropertyHistoriesService _assignedPropertyHistoriesService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AssignedPropertyHistoryController(IAssignedPropertyHistoriesService assignedPropertyHistoriesService,
           IMapper mapper)
        {
            _assignedPropertyHistoriesService = assignedPropertyHistoriesService;
            _mapper = mapper;
        }

        #endregion

        #region Public Method(s)

        [HttpGet(Name = "GetAssignedPropertyHistories")]
        [ProducesResponseType(typeof(AssignedPropertyHistoryModel), 200)]
        public async Task<IActionResult> Get([FromQuery]AssignedPropertyHistoryResourceParametersModel model)
        {
            var request = _mapper.Map<AssignedPropertyHistoryResourceParametersDto>(model);
            var results = await _assignedPropertyHistoriesService.GetAssignedPropertyHistories(request);
            var assignedPropertyHistoriesPagedResource = _mapper.Map<AssignedPropertyHistoryPagedResource>(results);
            return Ok(assignedPropertyHistoriesPagedResource);
        }

        [HttpGet("{id}", Name = "GetAssignedPropertyHistory")]
        [ProducesResponseType(typeof(AssignedPropertyHistoryModel), 200)]
        public async Task<IActionResult> GetAssignedPropertyHistory(Guid id)
        {
            var assignedPropertyHistoryResult = await _assignedPropertyHistoriesService.GetAssignedPropertyHistory(id);

            if (!assignedPropertyHistoryResult.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {assignedPropertyHistoryResult.Message}");
            }

            var assignedPropertyHistoryModel = _mapper.Map<AssignedPropertyHistoryModel>(assignedPropertyHistoryResult);

            return Ok(assignedPropertyHistoryModel);
        }

        [HttpPost(Name = "CreateAssignedPropertyHistory")]
        [ProducesResponseType(typeof(AssignedPropertyHistoryModel), 200)]
        public async Task<IActionResult> Post([FromBody]AssignedPropertyHistoryForCreationModel assignedPropertyHistory)
        {
            if (assignedPropertyHistory == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var assignedPropertyHistoryDto = _mapper.Map<AssignedPropertyHistoryForCreationDto>(assignedPropertyHistory);

            var result = await _assignedPropertyHistoriesService.CreateAssignedPropertyHistory(assignedPropertyHistoryDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpPut(Name = "UpdateAssignedPropertyHistory")]
        [ProducesResponseType(typeof(AssignedPropertyHistoryModel), 200)]
        public async Task<IActionResult> Put([FromBody]AssignedPropertyHistoryForUpdateModel assignedPropertyHistory)
        {
            if (assignedPropertyHistory == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var assignedPropertyHistoryDto = _mapper.Map<AssignedPropertyHistoryForUpdateDto>(assignedPropertyHistory);

            var result = await _assignedPropertyHistoriesService.UpdateAssignedPropertyHistory(assignedPropertyHistoryDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteAssignedPropertyHistory")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _assignedPropertyHistoriesService.DeleteAssignedPropertyHistory(new AssignedPropertyHistoryDeleteDto { AssignedPropertyHistoryId = id });

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        #endregion

    }
}
