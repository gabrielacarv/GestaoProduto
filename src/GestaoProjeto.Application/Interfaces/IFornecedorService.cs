using GestaoProjeto.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Application.Interfaces
{
    public interface IFornecedorService
    {
        Task<IEnumerable<FornecedorViewModel>> ObterTodos();
        Task<FornecedorViewModel> ObterPorId(int id);
        Task<IEnumerable<FornecedorViewModel>> ObterPorCategoria(int codigo);

        void Adicionar(NovoFornecedorViewModel novoFornecedor);
        bool Atualizar(NovoFornecedorViewModel novoFornecedor);
        bool Deletar(int id);
    }
}
