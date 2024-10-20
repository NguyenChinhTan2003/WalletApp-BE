using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp3.BillDetails
{
    public class TotalToDateDto
    {

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Total { get; set; }
    }
}
