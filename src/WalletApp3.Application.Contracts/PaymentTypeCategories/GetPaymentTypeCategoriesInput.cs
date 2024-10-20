using Volo.Abp.Application.Dtos;
using System;

namespace WalletApp3.PaymentTypeCategories
{
    public abstract class GetPaymentTypeCategoriesInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? CategoryName { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }

        public GetPaymentTypeCategoriesInputBase()
        {

        }
    }
}