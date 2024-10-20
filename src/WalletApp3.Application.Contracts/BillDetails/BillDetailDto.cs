using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace WalletApp3.BillDetails
{
    public abstract class BillDetailDtoBase : FullAuditedEntityDto<int>, IHasConcurrencyStamp
    {
        public decimal Money { get; set; }
        public string Note { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public Guid UserId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}