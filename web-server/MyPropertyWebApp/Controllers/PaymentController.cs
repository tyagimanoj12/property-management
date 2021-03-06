using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProperty.Services.DTOs;
using MyProperty.Services.Interfaces;
using MyProperty.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProperty.Web.Controllers
{
    [Route("api/payments")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        #region Fields

        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public PaymentController(IPaymentService paymentService,
           IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        [HttpGet(Name = "GetPayments")]
        [ProducesResponseType(typeof(PaymentModel), 200)]
        public async Task<IActionResult> Get([FromQuery]PaymentResourceParametersModel model)
        {
            var request = _mapper.Map<PaymentResourceParametersDto>(model);
            var results = await _paymentService.GetPayments(request);
            var paymentsPagedResource = _mapper.Map<PaymentPagedResource>(results);
            return Ok(paymentsPagedResource);
        }

        [HttpGet("{id}", Name = "GetPayment")]
        [ProducesResponseType(typeof(PaymentModel), 200)]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            var paymentResult = await _paymentService.GetPayment(id);

            if (!paymentResult.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {paymentResult.Message}");
            }

            var paymentModel = _mapper.Map<PaymentModel>(paymentResult);

            return Ok(paymentModel);
        }

        [HttpPost(Name = "CreatePayment")]
        [ProducesResponseType(typeof(PaymentModel), 200)]
        public async Task<IActionResult> Post([FromBody]PaymentForCreationModel payment)
        {
            if (payment == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var paymentDto = _mapper.Map<PaymentForCreationDto>(payment);

            var result = await _paymentService.CreatePayment(paymentDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpPut(Name = "UpdatePayment")]
        [ProducesResponseType(typeof(PaymentModel), 200)]
        public async Task<IActionResult> Put([FromBody]PaymentForUpdateModel payment)
        {
            if (payment == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var paymentDto = _mapper.Map<PaymentForUpdateDto>(payment);

            var result = await _paymentService.UpdatePayment(paymentDto);

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeletePayment")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _paymentService.DeletePayment(new PaymentDeleteDto { PaymentId = id });

            if (!result.Success)
            {
                return StatusCode(500, $"A problem happened with handling your request. Error : {result.Message}");
            }

            return Ok(result);
        }

        #endregion

    }
}
