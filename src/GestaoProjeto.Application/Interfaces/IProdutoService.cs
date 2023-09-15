using GestaoProjeto.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();
        Task<ProdutoViewModel> ObterPorId(int id);
        Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo);

        void Adicionar(NovoProdutoViewModel novoProdutoViewModel);
        bool Atualizar(NovoProdutoViewModel novoProdutoViewModel);
        bool Deletar(int id);
    }
}
