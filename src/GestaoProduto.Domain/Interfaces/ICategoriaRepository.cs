using GestaoProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> ObterTodos();
        Task<Categoria> ObterPorId(int id);
        Task<IEnumerable<Categoria>> ObterPorCategoria(string nomeCategoria);
        Task Adicionar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        bool Deletar(int id);
        Task AlterarDescricao(Categoria categoria, string novaDescricao);
    }
}
