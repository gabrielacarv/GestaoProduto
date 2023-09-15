using GestaoProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterPorId(int id);
        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);

        void Adicionar(Produto novoproduto);
        bool Atualizar(Produto produto);
        bool Deletar(int id);
    }
}