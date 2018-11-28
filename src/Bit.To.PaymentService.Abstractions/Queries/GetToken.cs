namespace Bit.To.PaymentService.Abstractions.Queries
{
    public class GetToken : Query<FermaAuthDto>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

}
