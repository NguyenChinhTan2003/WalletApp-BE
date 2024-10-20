using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp3
{
    public class UserLoginDto
    {
        public bool IsSucess { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
    }
}
