using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp3.BillDetails
{
    public class TotalCategoryDto
    {
        public int CategoryId { get; set; }
    
        public decimal Money { get; set; }

        public string CategoryName { get; set; }
    }
}
