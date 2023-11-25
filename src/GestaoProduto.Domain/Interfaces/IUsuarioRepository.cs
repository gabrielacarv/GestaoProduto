using GestaoProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<Usuario> Autenticar (string login, string senha);
        public Task Cadastrar(Usuario usuario);
    }
}
