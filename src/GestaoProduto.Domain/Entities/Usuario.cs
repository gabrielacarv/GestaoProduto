using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Domain.Entities
{
    public class Usuario
    {
        #region Construtor
        public Usuario(string login, string senha, bool ativo)
        {
            Login = login;
            Senha = senha;
            Ativo = ativo;
        }
        #endregion

        #region Propriedade
        public string Login { get; private set; }
        public string Senha { get;  set; }
        public bool Ativo { get; private set; }
        #endregion
    }
}
