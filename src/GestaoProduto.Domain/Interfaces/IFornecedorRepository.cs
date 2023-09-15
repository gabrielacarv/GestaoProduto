using GestaoProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Domain.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<IEnumerable<Fornecedor>> ObterTodos();
        Task<Fornecedor> ObterPorId(int id);
        Task<IEnumerable<Fornecedor>> ObterPorCategoria(int codigo);

        void Adicionar(Fornecedor fornecedor);
        bool Atualizar(Fornecedor fornecedor);
        bool Deletar(int id);
    }
}
