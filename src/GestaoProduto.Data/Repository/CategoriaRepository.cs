using GestaoProduto.Domain.Entities;
using GestaoProduto.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Data.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string _categoriaCaminhoArquivo;

        #region Construtores

        public CategoriaRepository()
        {
            _categoriaCaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "FileJsonData", "categoria.json"); ;
        }

        #endregion

        #region Funções do Arquivo 
        public Task<IEnumerable<Categoria>> ObterTodos()
        {
            List<Categoria> categorias = LerCategoriasDoArquivo();
            return Task.FromResult<IEnumerable<Categoria>>(categorias);
        }

        public async Task<Categoria> ObterPorId(int id)
        {
            List<Categoria> categorias = LerCategoriasDoArquivo();
            return await Task.FromResult(categorias.FirstOrDefault(p => p.Codigo == id));
        }

        public Task<IEnumerable<Categoria>> ObterPorCategoria(int codigo)
        {
            throw new NotImplementedException();
        }

        public void Adicionar(Categoria categoria)
        {
            List<Categoria> categorias = LerCategoriasDoArquivo();
            int proximoCodigo = ObterProximoCodigoDisponivel();
            categorias.Add(categoria);
            EscreverCategoriaNoArquivo(categorias);
        }

        public bool Atualizar(Categoria categoria)
        {
            List<Categoria> categorias = LerCategoriasDoArquivo();
            var categoriaExistente = categorias.FirstOrDefault(p => p.Codigo == categoria.Codigo);
            if (categoriaExistente != null)
            {
                categoriaExistente.AlterarDescricao(categoria.Descricao);

                EscreverCategoriaNoArquivo(categorias);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Deletar(int id)
        {
            List<Categoria> categorias = LerCategoriasDoArquivo();
            var categoriaExistente = categorias.FirstOrDefault(p => p.Codigo == id);
            if (categoriaExistente != null)
            {
                categorias.Remove(categoriaExistente);
                EscreverCategoriaNoArquivo(categorias);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Métodos do Arquivo

        private List<Categoria> LerCategoriasDoArquivo()
        {
            if (!System.IO.File.Exists(_categoriaCaminhoArquivo))
            {
                return new List<Categoria>();
            }

            string json = System.IO.File.ReadAllText(_categoriaCaminhoArquivo);
            return JsonConvert.DeserializeObject<List<Categoria>>(json);
        }

        private int ObterProximoCodigoDisponivel()
        {
            List<Categoria> categorias = LerCategoriasDoArquivo();
            if (categorias.Any())
            {
                return categorias.Max(p => p.Codigo) + 1;
            }
            else
            {
                return 1;
            }
        }

        private void EscreverCategoriaNoArquivo(List<Categoria> categorias)
        {
            string json = JsonConvert.SerializeObject(categorias);
            System.IO.File.WriteAllText(_categoriaCaminhoArquivo, json);
        }
        #endregion
    }
}
