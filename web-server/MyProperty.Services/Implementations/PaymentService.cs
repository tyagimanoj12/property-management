using Microsoft.EntityFrameworkCore;
using MyProperty.Data;
using MyProperty.Data.Entities;
using MyProperty.Services.DTOs;
using MyProperty.Services.Helpers;
using MyProperty.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProperty.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        #region Fields

        private readonly MyPropertyContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IAuthContext _authContext;

        #endregion

        #region Constructor

        public PaymentService(MyPropertyContext context, IPropertyMappingService propertyMappingService,
            IAuthContext authContext)
        {
            _context = context;
            _propertyMappingService = propertyMappingService;
            _authContext = authContext;
        }

        #endregion


        public async Task<PaymentResponseDto> CreatePayment(PaymentForCreationDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var ownerId = _authContext.GetAuthOwnerId();

            var response = new PaymentResponseDto();

            try
            {
                // create payment object for creation
                var payment = new Payment
                {
                    PaymentId = Guid.NewGuid(),
                    TenantId = request.TenantId,
                    PropertyId = request.PropertyId,
                    PropertyOwnerId = request.PropertyOwnerId,
                    Amount = request.Amount,
                    Credit = request.Credit,
                    Debit = request.Debit,
                    CreatedBy = ownerId ?? Guid.Empty,
                    CreatedOn = DateTime.Now,
                };

                // add to context
                await _context.Payments.AddAsync(payment);
                // commit 
                _context.SaveChanges();

                // got so far 
                response.Success = true;
                response.PaymentId = payment.PaymentId;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public async Task<PaymentDeleteResponse> DeletePayment(PaymentDeleteDto request)
        {
            var response = new PaymentDeleteResponse();
            try
            {
                var payment = await _context.Payments
                    .FirstOrDefaultAsync(x => x.PaymentId == request.PaymentId);

                if (payment == null)
                {
                    response.Message = "This payment does not exist in the system.";
                    return response;
                }

                payment.Deleted = true;

                _context.SaveChanges();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<PaymentDto> GetPayment(Guid id)
        {
            var response = new PaymentDto();
            try
            {
                var payment = await _context.Payments
                    .FirstOrDefaultAsync(x => x.PaymentId == id);

                if (payment == null)
                {
                    response.Message = "This payment does not exist in the system.";
                    return response;
                }

                response.TenantId = payment.TenantId;
                response.PropertyOwnerId = payment.PropertyOwnerId;
                response.Amount = payment.Amount;
                response.Credit = payment.Credit;
                response.Debit = payment.Debit;
                response.PropertyId = payment.PropertyId;
                response.PaymentId = payment.PaymentId;
                response.PaymentType = payment.Debit == true ? 1 : 2;
                response.Success = true;
                response.Message = "";
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<PagedList<PaymentDataDto>> GetPayments(PaymentResourceParametersDto paymentResourceParameters)
        {
            _propertyMappingService.InitPropertyMapping<PaymentDto, Payment>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "PaymentId", new PropertyMappingValue(new List<string>(){ "PaymentId" }) },
                { "PropertyOwnerId", new PropertyMappingValue(new List<string>(){ "PropertyOwnerId" }) },
                { "PropertyId", new PropertyMappingValue(new List<string>(){ "PropertyId" }) },
                { "TenantId", new PropertyMappingValue(new List<string>(){ "TenantId" }, true) },
                { "Amount", new PropertyMappingValue(new List<string>(){ "Amount" }, true) },
                { "Debit", new PropertyMappingValue(new List<string>(){ "Debit" }) },
                { "Credit", new PropertyMappingValue(new List<string>(){ "Credit" }) }
            });

            var query = _context.Payments.AsQueryable();

            if (paymentResourceParameters.PropertyOwnerId != null)
            {
                 query = query.Include(x => x.Tenant).Include(y => y.Property)
                         .ThenInclude(z => z.PropertyOwner).Where(x => x.PropertyOwnerId == paymentResourceParameters.PropertyOwnerId && !x.Deleted)
                         .ApplySort(paymentResourceParameters.OrderBy, _propertyMappingService.GetPropertyMapping<PaymentDto, Payment>());
            }
            else if (paymentResourceParameters.TenantId != null)
            {
                 query = query.Include(x => x.Tenant).Include(y => y.Property)
                        .ThenInclude(z => z.PropertyOwner).Where(x => x.TenantId == paymentResourceParameters.TenantId && !x.Deleted)
                        .ApplySort(paymentResourceParameters.OrderBy, _propertyMappingService.GetPropertyMapping<PaymentDto, Payment>());
            }
            else
            {
                 query = query.Include(x => x.Tenant).Include(y => y.Property)
                          .ThenInclude(z => z.PropertyOwner).Where(x => !x.Deleted)
                          .ApplySort(paymentResourceParameters.OrderBy, _propertyMappingService.GetPropertyMapping<PaymentDto, Payment>());
            }

            var result = query.Select(x => new PaymentDataDto
            {
                PropertyOwnerName = x.PropertyOwner.Name,
                PropertyName = x.Property.Name,
                TenantName = x.Tenant.Name,
                PropertyOwnerId = x.PropertyOwnerId,
                PropertyId = x.PropertyId,
                TenantId = x.TenantId,
                Amount = x.Amount,
                Debit = x.Debit,
                Credit = x.Credit,
                PaymentType = x.Debit == true ? "Debit" : "Credit",
                PaymentId = x.PaymentId,
                DateCreated = x.CreatedOn
            }).OrderByDescending(x => x.DateCreated);

            if (!string.IsNullOrEmpty(paymentResourceParameters.SearchQuery))
            {
                // trim and ignore casing
                var searchQueryForWhereClause = paymentResourceParameters.SearchQuery.TrimEnd().ToLowerInvariant();

                query = query
                    .Where(x => x.Tenant.Name.ToLowerInvariant().Contains(searchQueryForWhereClause));
            }

            return PagedList<PaymentDataDto>.Create(result,
                paymentResourceParameters.PageNumber,
                paymentResourceParameters.PageSize);
        }

        public async Task<PaymentResponseDto> UpdatePayment(PaymentForUpdateDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = new PaymentResponseDto();

            var ownerId = _authContext.GetAuthOwnerId();

            try
            {
                if (request.TenantId != Guid.Empty)
                {
                    var payment = await _context.Payments.FirstOrDefaultAsync(x => x.PaymentId == request.PaymentId);

                    // get payment object for update
                    if (payment != null)
                    {
                        payment.Amount = request.Amount;
                        payment.Debit = request.Debit;
                        payment.Credit = request.Credit;
                        payment.PropertyId = request.PropertyId;
                        payment.PropertyOwnerId = request.PropertyOwnerId;
                        payment.TenantId = request.TenantId;
                        payment.UpdatedBy = ownerId ?? Guid.Empty;
                        payment.UpdatedOn = DateTime.Now;
                    }

                    // commit 
                    _context.SaveChanges();
                    // got so far 
                    response.PaymentId = payment.PaymentId;

                    response.Success = true;

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }
    }
}
