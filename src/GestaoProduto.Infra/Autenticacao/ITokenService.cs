using GestaoProduto.Infra.Autenticacao.Models;
using System.IdentityModel.Tokens.Jwt;

namespace GestaoProduto.Infra.Autenticacao
{
    public interface ITokenService
    {
        Task<string> GerarTokenJWT(TokenRequest request);
        Task<JwtSecurityToken> DecodificarTokenJWT(string protectedText);
    }
}
