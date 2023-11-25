using GestaoProduto.Domain.Entities;
using GestaoProduto.Infra;
using Microsoft.Extensions.Options;

namespace GestaoProjeto.Application.Services
{
    public class EmailService
    {
        private readonly EmailConfig _emailConfig;

        public EmailService(IOptions<EmailConfig> options)
        {
            _emailConfig = options.Value;
        }

        public void VerificarEstoqueEEnviarEmail(Produto produto, int estoqueMinimo)
        {

            string assunto = "Aviso de Estoque Baixo";
            string destinatario = "gabriel_alvescarvalho@hotmail.com";
            string corpoEmailModelo = "Olá, o produto '{nomeProduto}' está abaixo do estoque mínimo {estoqueMinimo} definido no sistema, logo, você precisa avaliar se é necessário realizar um novo pedido de compra.";

            string corpoEmail = corpoEmailModelo
                .Replace("{nomeProduto}", produto.Nome)
                .Replace("{estoqueMinimo}", estoqueMinimo.ToString());

            Email.Enviar(assunto, corpoEmail, destinatario, _emailConfig);    
            
        }
    }
}
