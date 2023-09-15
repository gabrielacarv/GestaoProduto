using GestaoProduto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoProduto.Domain.Entities;
using Newtonsoft.Json;

namespace GestaoProduto.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        #region Construtores

        private readonly string _produtoCaminhoArquivo;

        public ProdutoRepository()
        {
            _produtoCaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "FileJsonData", "produto.json");
        }
        #endregion

        #region Funções do Arquivo      
        public Task<IEnumerable<Produto>> ObterTodos()
        {
            List<Produto> produtos = LerProdutosDoArquivo();
            return Task.FromResult<IEnumerable<Produto>>(produtos);
        }

        public async Task<Produto> ObterPorId(int id)
        {
            List<Produto> produtos = LerProdutosDoArquivo();
            return await Task.FromResult(produtos.FirstOrDefault(p => p.Codigo == id));
        }

        public Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
        {
            //List<Produto> produtos = LerProdutosDoArquivo();
            //return produtos.Where(p => p.CategoriaCodigo == codigo);
            throw new NotImplementedException();
        }

        public void Adicionar(Produto novoproduto)
        {
            //List<Produto> produtos = new List<Produto>();
            List<Produto> produtos = LerProdutosDoArquivo();
            int proximoCodigo = ObterProximoCodigoDisponivel();
            produtos.Add(novoproduto);
            EscreverProdutosNoArquivo(produtos);
        }

        public bool Atualizar(Produto produto)
        {
            List<Produto> produtos = LerProdutosDoArquivo();
            var produtoExistente = produtos.FirstOrDefault(p => p.Codigo == produto.Codigo);
            if (produtoExistente != null)
            {
                produtoExistente.AlterarNome(produto.Nome);
                produtoExistente.AlterarDescricao(produto.Descricao);
                EscreverProdutosNoArquivo(produtos);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Deletar(int id)
        {
            List<Produto> produtos = LerProdutosDoArquivo();
            var produtoExistente = produtos.FirstOrDefault(p => p.Codigo == id);
            if (produtoExistente != null)
            {
                produtos.Remove(produtoExistente);
                EscreverProdutosNoArquivo(produtos);
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #region Métodos do Arquivo
        private List<Produto> LerProdutosDoArquivo()
        {
            if (!System.IO.File.Exists(_produtoCaminhoArquivo))
            {
                return new List<Produto>();
            }

            string json = System.IO.File.ReadAllText(_produtoCaminhoArquivo);
            return JsonConvert.DeserializeObject<List<Produto>>(json);
        }

        private int ObterProximoCodigoDisponivel()
        {
            List<Produto> produtos = LerProdutosDoArquivo();
            if (produtos.Any())
            {
                return produtos.Max(p => p.Codigo) + 1;
            }
            else
            {
                return 1;
            }
        }

        private void EscreverProdutosNoArquivo(List<Produto> produtos)
        {
            string json = JsonConvert.SerializeObject(produtos);
            System.IO.File.WriteAllText(_produtoCaminhoArquivo, json);
        }
        #endregion
    }
}
