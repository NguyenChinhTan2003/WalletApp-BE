using AutoMapper;
using System;
using Volo.Abp.Identity;
using WalletApp3.BillDetails;
using WalletApp3.PaymentTypeCategories;
using WalletApp3.Shared;

namespace WalletApp3;

public class WalletApp3ApplicationAutoMapperProfile : Profile
{
    public WalletApp3ApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<PaymentTypeCategory, PaymentTypeCategoryDto>();
        CreateMap<PaymentTypeCategory, PaymentTypeCategoryExcelDto>();

        CreateMap<BillDetail, BillDetailDto>();
        CreateMap<BillDetail, BillDetailExcelDto>();
        CreateMap<TotalBillDetailViewModel, TotalBillDetailDto>();
        CreateMap<TotalCategoryDateViewModel, TotalCategoryDateDto>();
        CreateMap<TotalCategoryViewModel, TotalCategoryDto>();
        CreateMap<TotalToDayViewModel, TotalToDayDto>();
        CreateMap<TotalToDateViewModel, TotalToDateDto>();



        CreateMap<BillDetailWithNavigationProperties, BillDetailWithNavigationPropertiesDto>();
        CreateMap<PaymentTypeCategory, LookupDto<int>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.CategoryName));
        CreateMap<IdentityUser, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
    }
}
