using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WalletApp3.BillDetails
{
    public abstract class BillDetailCreateDtoBase
    {
        public decimal Money { get; set; }
        [Required]
        [StringLength(BillDetailConsts.NoteMaxLength, MinimumLength = BillDetailConsts.NoteMinLength)]
        public string Note { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}