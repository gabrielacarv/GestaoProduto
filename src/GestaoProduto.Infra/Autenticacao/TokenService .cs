using GestaoProduto.Infra.Autenticacao.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestaoProduto.Infra.Autenticacao
{
    public class TokenService : ITokenService
    {
        private readonly Token _token;


        public TokenService(IOptions<Token> token)
        {
            _token = token.Value;
        }

        public async Task<string> GerarTokenJWT(TokenRequest request)
        {
            byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);
            //token expira no fim do dia (23:59:59)
            DateTime expirationDateTime = DateTime.UtcNow.Date
                .AddHours(23).AddMinutes(59).AddSeconds(59);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("usuario", request.usuario.ToString()),
                }),

                Expires = expirationDateTime,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);

        }

        public async Task<JwtSecurityToken> DecodificarTokenJWT(string protectedText)
        {
            if (String.IsNullOrEmpty(protectedText))
            {
                return null;
            }

            protectedText = protectedText.Replace("Bearer ", "");
            protectedText = protectedText.Replace("bearer ", "");

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _token.Issuer,
                ValidAudience = _token.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Convert.ToString(_token.Secret)))
            };

            var handler = new JwtSecurityTokenHandler();

            var principal = await handler.ValidateTokenAsync(protectedText, validationParameters);

            return (JwtSecurityToken)principal.SecurityToken;
        }
    }
}
