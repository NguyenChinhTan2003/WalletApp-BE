using Volo.Abp.Application.Dtos;
using System;

namespace WalletApp3.BillDetails
{
    public abstract class GetBillDetailsInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public decimal? MoneyMin { get; set; }
        public decimal? MoneyMax { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAtMin { get; set; }
        public DateTime? CreatedAtMax { get; set; }
        public int? CategoryId { get; set; }
        public Guid? UserId { get; set; }

        public GetBillDetailsInputBase()
        {

        }
    }
}