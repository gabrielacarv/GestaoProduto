using GestaoProjeto.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Application.Interfaces
{
    public interface IUsuarioService
    {
        public Task<string> Autenticar(AutenticarUsuarioViewModel autenticarUsuarioViewModel);
        public Task<string> Autenticar2(AutenticarUsuarioViewModel autenticarUsuarioViewModel);
        public Task Cadastrar(UsuarioViewModel usuario);
    }
}
