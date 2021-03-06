using MyProperty.Data.Entities;
using MyProperty.Services.DTOs;
using MyProperty.Services.Helpers;
using System;
using System.Threading.Tasks;

namespace MyProperty.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> CreatePayment(PaymentForCreationDto request);
        Task<PaymentResponseDto> UpdatePayment(PaymentForUpdateDto request);
        Task<PaymentDto> GetPayment(Guid id);
        Task<PagedList<PaymentDataDto>> GetPayments(PaymentResourceParametersDto paymentResourceParameters);
        Task<PaymentDeleteResponse> DeletePayment(PaymentDeleteDto request);
    }
}
