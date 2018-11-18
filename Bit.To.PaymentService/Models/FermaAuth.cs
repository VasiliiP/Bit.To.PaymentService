using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.PaymentService.Models
{
    public class FermaAuth
    {
        public string AuthToken { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(AuthToken))
                    return false;

                return ExpirationDateUtc > DateTime.UtcNow;
            }
        }

        public static FermaAuth FromResponse(FermaAuthResponse response)
        {
            if (response.Data == null)
                return null;

            var result = new FermaAuth
            {
                AuthToken = response.Data.AuthToken,
                ExpirationDateUtc = response.Data.ExpirationDateUtc
            };
            return result;
        }
    }
}
