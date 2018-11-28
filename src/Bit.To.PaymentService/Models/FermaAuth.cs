using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.PaymentService.Models
{
    public class FermaAuth
    {
        public string AuthToken { get; }
        public DateTime ExpirationDateUtc { get; }
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(AuthToken))
                    return false;

                return ExpirationDateUtc > DateTime.UtcNow;
            }
        }

        public static FermaAuth CreateNew(string token, DateTime expDate)
        {
            return new FermaAuth(token, expDate);
        }

        private FermaAuth(string token, DateTime expDate)
        {
            AuthToken = token;
            ExpirationDateUtc = expDate;
        }
    }
}
