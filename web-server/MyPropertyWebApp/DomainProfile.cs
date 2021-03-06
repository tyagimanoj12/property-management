using AutoMapper;
using MyProperty.Data.Entities;
using MyProperty.Services.DTOs;
using MyProperty.Services.Helpers;
using MyProperty.Web.Models;
using System.Linq;

namespace MyProperty.Web
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            #region Owners

            CreateMap<OwnerModel, OwnerDto>().ReverseMap();
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<ValidatePasswordModel, OwnerPasswordValidateDto>().ReverseMap();
            CreateMap<UpdatePasswordModel, UpdatePasswordDto>().ReverseMap();

            #endregion

            #region Properties

            CreateMap<PropertyModel, PropertyDto>().ReverseMap();
            CreateMap<PropertyDto, Property>().ReverseMap();
            CreateMap<PropertyDataDto, PropertyModel>().ReverseMap();
            CreateMap<PropertyDataDto, Property>().ReverseMap();
            CreateMap<PropertyForCreationModel, PropertyForCreationDto>();
            CreateMap<PropertyForUpdateModel, PropertyForUpdateDto>();
            CreateMap<PropertyResourceParametersModel, PropertyResourceParametersDto>();
            CreateMap<PagedList<Data.Entities.Property>, PropertyPagedResource>()
               .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.ToList()))
               .ForMember(dest => dest.Page, opt => opt.MapFrom(src => new Page
               {
                   PageNumber = src.CurrentPage,
                   Size = src.PageSize,
                   TotalElements = src.TotalCount,
                   TotalPages = src.TotalPages
               }));

            #endregion

            #region Tenants

            CreateMap<TenantModel, TenantDto>().ReverseMap();
            CreateMap<TenantDto, Tenant>().ReverseMap();
            CreateMap<TenantForCreationModel, TenantForCreationDto>();
            CreateMap<TenantForUpdateModel, TenantForUpdateDto>();
            CreateMap<TenantResourceParametersModel, TenantResourceParametersDto>();
            CreateMap<PagedList<Tenant>, TenantPagedResource>()
               .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.ToList()))
               .ForMember(dest => dest.Page, opt => opt.MapFrom(src => new Page
               {
                   PageNumber = src.CurrentPage,
                   Size = src.PageSize,
                   TotalElements = src.TotalCount,
                   TotalPages = src.TotalPages
               }));

            #endregion

            #region AssignedProperties

            CreateMap<AssignedPropertyModel, AssignedPropertyDto>().ReverseMap();
            CreateMap<AssignedPropertyDataDto, AssignedPropertyModel>().ReverseMap();
            CreateMap<AssignedPropertyDto, AssignedProperty>().ReverseMap();
            CreateMap<AssignedPropertyForCreationModel, AssignedPropertyForCreationDto>();
            CreateMap<AssignedPropertyForUpdateModel, AssignedPropertyForUpdateDto>();
            CreateMap<AssignedPropertyResourceParametersModel, AssignedPropertyResourceParametersDto>();
            CreateMap<PagedList<AssignedPropertyDataDto>, AssignedPropertyPagedResource>()
               .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.ToList()))
               .ForMember(dest => dest.Page, opt => opt.MapFrom(src => new Page
               {
                   PageNumber = src.CurrentPage,
                   Size = src.PageSize,
                   TotalElements = src.TotalCount,
                   TotalPages = src.TotalPages
               }));

            #endregion

            #region AssignedPropertyHistories

            CreateMap<AssignedPropertyHistoryModel, AssignedPropertyHistoryDto>().ReverseMap();
            CreateMap<AssignedPropertyHistoryDto, AssignedPropertyHistory>().ReverseMap();
            CreateMap<AssignedPropertyHistoryForCreationModel, AssignedPropertyHistoryForCreationDto>();
            CreateMap<AssignedPropertyHistoryForUpdateModel, AssignedPropertyHistoryForUpdateDto>();
            CreateMap<AssignedPropertyHistoryResourceParametersModel, AssignedPropertyHistoryResourceParametersDto>();
            CreateMap<PagedList<AssignedPropertyHistory>, AssignedPropertyHistoryPagedResource>()
               .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.ToList()))
               .ForMember(dest => dest.Page, opt => opt.MapFrom(src => new Page
               {
                   PageNumber = src.CurrentPage,
                   Size = src.PageSize,
                   TotalElements = src.TotalCount,
                   TotalPages = src.TotalPages
               }));

            #endregion

            #region PropertyOwners

            CreateMap<PropertyOwnerModel, PropertyOwnerDto>().ReverseMap();
            CreateMap<PropertyOwnerDto, PropertyOwner>().ReverseMap();
            CreateMap<PropertyOwnerForCreationModel, PropertyOwnerForCreationDto>();
            CreateMap<PropertyOwnerForUpdateModel, PropertyOwnerForUpdateDto>();
            CreateMap<PropertyOwnerResourceParametersModel, PropertyOwnerResourceParametersDto>();
            CreateMap<PagedList<PropertyOwner>, PropertyOwnerPagedResource>()
               .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.ToList()))
               .ForMember(dest => dest.Page, opt => opt.MapFrom(src => new Page
               {
                   PageNumber = src.CurrentPage,
                   Size = src.PageSize,
                   TotalElements = src.TotalCount,
                   TotalPages = src.TotalPages
               }));

            #endregion

            #region Payments

            CreateMap<PaymentModel, PaymentDto>().ReverseMap();
            CreateMap<PaymentDataDto, PaymentModel>().ReverseMap();
            CreateMap<PaymentDto, Payment>().ReverseMap();
            CreateMap<PaymentForCreationModel, PaymentForCreationDto>();
            CreateMap<PaymentForUpdateModel, PaymentForUpdateDto>();
            CreateMap<PaymentResourceParametersModel, PaymentResourceParametersDto>();
            CreateMap<PagedList<PaymentDataDto>, PaymentPagedResource>()
               .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.ToList()))
               .ForMember(dest => dest.Page, opt => opt.MapFrom(src => new Page
               {
                   PageNumber = src.CurrentPage,
                   Size = src.PageSize,
                   TotalElements = src.TotalCount,
                   TotalPages = src.TotalPages
               }));

            #endregion

            #region Dashboard 

            CreateMap<DashboardDataDto, DashboardDataModel>().ReverseMap();

            #endregion

        }
    }
}
