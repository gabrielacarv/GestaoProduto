using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Domain.Entities
{
    public class Usuario
    {
        public Usuario(string login, string senha, bool ativo)
        {
            Login = login;
            Senha = senha;
            Ativo = ativo;
        }

        public string Login { get; private set; }
        public string Senha { get; private set; }
        public bool Ativo { get; private set; }   
    }
}
