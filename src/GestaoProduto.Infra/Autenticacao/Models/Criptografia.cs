using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Infra.Autenticacao.Models
{
    public class Criptografia
    {
        public static string CriptografarSenha(string senha)
        {
            // Gere um salt (valor aleatório único para cada senha)
            byte[] salt = GerarSalt();

            // Combine a senha com o salt
            byte[] senhaBytes = Encoding.UTF8.GetBytes(senha);
            byte[] senhaComSalt = new byte[senhaBytes.Length + salt.Length];
            Buffer.BlockCopy(senhaBytes, 0, senhaComSalt, 0, senhaBytes.Length);
            Buffer.BlockCopy(salt, 0, senhaComSalt, senhaBytes.Length, salt.Length);

            // Use o algoritmo SHA-256 para gerar o hash
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(senhaComSalt);

                // Combine o salt com o hash e converta para uma string base64 para armazenamento
                byte[] hashComSalt = new byte[hashBytes.Length + salt.Length];
                Buffer.BlockCopy(hashBytes, 0, hashComSalt, 0, hashBytes.Length);
                Buffer.BlockCopy(salt, 0, hashComSalt, hashBytes.Length, salt.Length);

                string hashBase64 = Convert.ToBase64String(hashComSalt);
                return hashBase64;
            }
        }

        private static byte[] GerarSalt()
        {
            // Gere um salt aleatório, você pode personalizar a lógica conforme necessário
            byte[] salt = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
