using Bit.Domain;

namespace Bit.To.PaymentService.Models
{
    public class Cashbox : Entity<long>
    {
        public int DeviceId { get; }
        public string RNM { get; }
        public string ZN { get; }
        public string FN { get; }
        public string FDN { get; }
        public string FPD { get; }

        private Cashbox(int deviceId, string rNM, string zN, string fN, string fDN, string fPD)
        {
            DeviceId = deviceId;
            RNM = rNM;
            ZN = zN;
            FN = fN;
            FDN = fDN;
            FPD = fPD;
        }

        public static Cashbox CreateNew(int deviceId, string rNM, string zN, string fN, string fDN, string fPD)
        {
            return new Cashbox(deviceId, rNM, zN, fN, fDN, fPD);
        }

    }
}