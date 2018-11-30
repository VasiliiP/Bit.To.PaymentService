namespace Bit.To.PaymentService.Abstractions.Queries
{
    public class GetToken : Query<string>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

}
