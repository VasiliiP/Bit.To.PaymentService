namespace Bit.To.YaKassa.RestClients.Models
{
    public enum YaKassaErrorEnum
    {
        InvalidRequest = 400,
        InvalidCredentials = 401,
        Forbidden = 403,
        NotFound = 404,
        TooManyRequests = 429,
        InternalServerError = 500
    }
}
