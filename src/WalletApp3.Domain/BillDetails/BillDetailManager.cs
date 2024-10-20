using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace WalletApp3.BillDetails
{
    public abstract class BillDetailManagerBase : DomainService
    {
        protected IBillDetailRepository _billDetailRepository;

        public BillDetailManagerBase(IBillDetailRepository billDetailRepository)
        {
            _billDetailRepository = billDetailRepository;
        }

        public virtual async Task<BillDetail> CreateAsync(
        int categoryId, Guid userId, decimal money, string note, DateTime createdAt)
        {
            Check.NotNull(categoryId, nameof(categoryId));
            Check.NotNull(userId, nameof(userId));
            Check.NotNullOrWhiteSpace(note, nameof(note));
            Check.Length(note, nameof(note), BillDetailConsts.NoteMaxLength, BillDetailConsts.NoteMinLength);
            Check.NotNull(createdAt, nameof(createdAt));

            var billDetail = new BillDetail(

             categoryId, userId, money, note, createdAt
             );

            return await _billDetailRepository.InsertAsync(billDetail);
        }

        public virtual async Task<BillDetail> UpdateAsync(
            int id,
            int categoryId, Guid userId, decimal money, string note, DateTime createdAt, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(categoryId, nameof(categoryId));
            Check.NotNull(userId, nameof(userId));
            Check.NotNullOrWhiteSpace(note, nameof(note));
            Check.Length(note, nameof(note), BillDetailConsts.NoteMaxLength, BillDetailConsts.NoteMinLength);
            Check.NotNull(createdAt, nameof(createdAt));

            var billDetail = await _billDetailRepository.GetAsync(id);

            billDetail.CategoryId = categoryId;
            billDetail.UserId = userId;
            billDetail.Money = money;
            billDetail.Note = note;
            billDetail.CreatedAt = createdAt;

            billDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _billDetailRepository.UpdateAsync(billDetail);
        }

    }
}