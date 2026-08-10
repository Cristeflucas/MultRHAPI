using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace MultRH.Infrastructure.Pagamentos
{
    public class MercadoPagoWebhookValidator
    {
        private readonly IConfiguration _configuration;

        public MercadoPagoWebhookValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValid(string dataId, string xRequestId, string xSignatureHeader)
        {
            if (string.IsNullOrWhiteSpace(xSignatureHeader))
            {
                return false;
            }

            var partes = xSignatureHeader.Split(',')
                .Select(p => p.Split('=', 2))
                .Where(p => p.Length == 2)
                .ToDictionary(p => p[0].Trim(), p => p[1].Trim());

            if (!partes.TryGetValue("ts", out var ts) || !partes.TryGetValue("v1", out var v1))
            {
                return false;
            }

            var manifest = $"id:{dataId};request-id:{xRequestId};ts:{ts};";
            var secret = _configuration["MercadoPago:WebhookSecret"];

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(manifest));
            var hashHex = Convert.ToHexString(hash).ToLower();

            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(hashHex), Encoding.UTF8.GetBytes(v1));
        }
    }
}
