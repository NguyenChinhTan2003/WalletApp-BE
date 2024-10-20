using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp3.BillDetails
{
    /// <summary>
    /// Tổng tiền của ví
    /// </summary>
    public  class TotalBillDetailDto
    {
        /// <summary>
        /// Tong tien con lai
        /// </summary>
        public decimal TotalBalanace { get; set; }

        /// <summary>
        /// Tong thu
        /// </summary>
        public decimal TotalIncome{ get; set; }

        /// <summary>
        /// Tong chi
        /// </summary>
        public decimal TotalExpenses { get; set; }
    }
}
