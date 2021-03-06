using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProperty.Services.Interfaces;
using MyProperty.Web.Models;
using System;
using System.Threading.Tasks;

namespace MyProperty.Web.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        #region Fields

        //private readonly ;
        private readonly IMapper _mapper;
        private readonly IDashboardService _dashboardService;
        #endregion

        #region Constructor

        public DashboardController(IDashboardService dashboardService,
           IMapper mapper)
        {
            _dashboardService = dashboardService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        [HttpGet(Name = "GetDashboardData")]
        [ProducesResponseType(typeof(DashboardDataModel), 200)]
        public async Task<IActionResult> GetDashboardData()
        {
            var data = await _dashboardService.GetDashboardData();

            if (!data.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {data.Message}");
            }

            var result = _mapper.Map<DashboardDataModel>(data);

            return Ok(result);
        }


        #endregion
    }
}
