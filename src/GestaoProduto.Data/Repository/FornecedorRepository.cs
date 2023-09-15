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
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly string _fornecedorCaminhoArquivo;

        #region Construtores

        public FornecedorRepository()
        {
            _fornecedorCaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "FileJsonData", "fornecedor.json"); ;
        }

        #endregion

        #region Funções do Arquivo 
        public void Adicionar(Fornecedor fornecedor)
        {
            List<Fornecedor> fornecedores = LerFornecedoresDoArquivo();
            int proximoCodigo = ObterProximoCodigoDisponivel();
            fornecedores.Add(fornecedor);
            EscreverFornecedorNoArquivo(fornecedores);
        }

        public bool Atualizar(Fornecedor fornecedor)
        {
            List<Fornecedor> fornecedores = LerFornecedoresDoArquivo();
            var fornecedorExistente = fornecedores.FirstOrDefault(p => p.Codigo == fornecedor.Codigo);
            if (fornecedorExistente != null)
            {
                fornecedorExistente.AlterarRazaoSocial(fornecedor.RazaoSocial);
                fornecedorExistente.AlterarEmailContato(fornecedor.EmailContato);

                EscreverFornecedorNoArquivo(fornecedores);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Deletar(int id)
        {
            List<Fornecedor> fornecedores = LerFornecedoresDoArquivo();
            var fornecedorExistente = fornecedores.FirstOrDefault(p => p.Codigo == id);
            if (fornecedorExistente != null)
            {
                fornecedores.Remove(fornecedorExistente);
                EscreverFornecedorNoArquivo(fornecedores);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<IEnumerable<Fornecedor>> ObterPorCategoria(int codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<Fornecedor> ObterPorId(int id)
        {
            List<Fornecedor> fornecedores = LerFornecedoresDoArquivo();
            return await Task.FromResult(fornecedores.FirstOrDefault(p => p.Codigo == id));
        }

        public Task<IEnumerable<Fornecedor>> ObterTodos()
        {
            List<Fornecedor> fornecedores = LerFornecedoresDoArquivo();
            return Task.FromResult<IEnumerable<Fornecedor>>(fornecedores);
        }

        #endregion

        #region Métodos do Arquivo
        private List<Fornecedor> LerFornecedoresDoArquivo()
        {
            if (!System.IO.File.Exists(_fornecedorCaminhoArquivo))
            {
                return new List<Fornecedor>();
            }

            string json = System.IO.File.ReadAllText(_fornecedorCaminhoArquivo);
            return JsonConvert.DeserializeObject<List<Fornecedor>>(json);
        }

        private int ObterProximoCodigoDisponivel()
        {
            List<Fornecedor> fornecedores = LerFornecedoresDoArquivo();
            if (fornecedores.Any())
            {
                return fornecedores.Max(p => p.Codigo) + 1;
            }
            else
            {
                return 1;
            }
        }

        private void EscreverFornecedorNoArquivo(List<Fornecedor> fornecedores)
        {
            string json = JsonConvert.SerializeObject(fornecedores);
            System.IO.File.WriteAllText(_fornecedorCaminhoArquivo, json);
        }     
        #endregion
    }
}
