using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace WalletApp3.BillDetails
{
    public abstract class BillDetailUpdateDtoBase : IHasConcurrencyStamp
    {
        public decimal Money { get; set; }
        [Required]
        [StringLength(BillDetailConsts.NoteMaxLength, MinimumLength = BillDetailConsts.NoteMinLength)]
        public string Note { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public Guid UserId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}