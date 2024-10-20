using WalletApp3.PaymentTypeCategories;
using Volo.Abp.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace WalletApp3.BillDetails
{
    public abstract class BillDetailBase : FullAuditedAggregateRoot<int>
    {
        public virtual decimal Money { get; set; }

        [NotNull]
        public virtual string Note { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public Guid UserId { get; set; }

        protected BillDetailBase()
        {

        }

        public BillDetailBase(int categoryId, Guid userId, decimal money, string note, DateTime createdAt)
        {

            Check.NotNull(note, nameof(note));
            Check.Length(note, nameof(note), BillDetailConsts.NoteMaxLength, BillDetailConsts.NoteMinLength);
            Money = money;
            Note = note;
            CreatedAt = createdAt;
            CategoryId = categoryId;
            UserId = userId;
        }
    }
}