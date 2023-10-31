using GestaoProduto.Domain.Interfaces;
using GestaoProduto.Domain.Entities;
using Newtonsoft.Json;
using GestaoProduto.Data.Providers.MongoDb.Collections;
using GestaoProduto.Data.Providers.MongoDb.Interfaces;
using AutoMapper;
using MongoDB.Driver;

namespace GestaoProduto.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IMongoRepository<ProdutoCollection> _produtoRepository;

        private readonly string _produtoCaminhoArquivo;
        private readonly IMapper _mapper;

        #region - Construtores
        public ProdutoRepository(IMongoRepository<ProdutoCollection> produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }
        #endregion

        #region Funções do Arquivo      

        //public Task<IEnumerable<Produto>> ObterTodos()
        //{
        //    List<Produto> produtos = LerProdutosDoArquivo();
        //    return Task.FromResult<IEnumerable<Produto>>(produtos);
        //}

        public IEnumerable<Produto> ObterTodos()
        {
            var produtoList = _produtoRepository.FilterBy(filter => true);

            List<Produto> lista = new List<Produto>();
            foreach (var item in produtoList)
            {
                lista.Add(new Produto(item.Codigo, item.Nome, item.Descricao, item.Ativo, item.Valor, item.DataCadastro, item.Estoque));
            }

            //return _mapper.Map<IEnurable<Produto>>(produtoList);

            return lista;
        }

        //public async Task<Produto> ObterPorId(int id)
        //{
        //    List<Produto> produtos = LerProdutosDoArquivo();
        //    return await Task.FromResult(produtos.FirstOrDefault(p => p.Codigo == id));
        //    throw new NotImplementedException();
        //}

        public async Task<Produto> ObterPorId(int id)
        {
            var buscaProduto = _produtoRepository.FilterBy(filter => filter.Codigo == id);
            var produto = _mapper.Map<Produto>(buscaProduto.FirstOrDefault());
            return produto;
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Produto>> ObterPorNome(string nomeProduto)
        {
            var produtosEncontrados = _produtoRepository.FilterBy(filter => filter.Nome.Contains(nomeProduto));

            return _mapper.Map<IEnumerable<Produto>>(produtosEncontrados);
        }


        //public void Adicionar(Produto novoproduto)
        //{
        //    //List<Produto> produtos = new List<Produto>();
        //    List<Produto> produtos = LerProdutosDoArquivo();
        //    int proximoCodigo = ObterProximoCodigoDisponivel();
        //    produtos.Add(novoproduto);
        //    EscreverProdutosNoArquivo(produtos);
        //}

        public async Task Adicionar(Produto produto)
        {
            //await _produtoRepository.InsertOneAsync(_mapper.Map<ProdutoCollection>(produto)));

            ProdutoCollection produtoCollection = new ProdutoCollection();
            produtoCollection.Codigo = produto.Codigo;
            produtoCollection.Nome = produto.Nome;
            produtoCollection.Descricao = produto.Descricao;
            produtoCollection.Ativo = produto.Ativo;
            produtoCollection.Valor = produto.Valor;
            produtoCollection.DataCadastro = produto.DataCadastro;
            produtoCollection.Estoque = produto.Estoque;
            

            await _produtoRepository.InsertOneAsync(produtoCollection);
        }

        //public bool Atualizar(Produto produto)
        //{
        //    List<Produto> produtos = LerProdutosDoArquivo();
        //    var produtoExistente = produtos.FirstOrDefault(p => p.Codigo == produto.Codigo);
        //    if (produtoExistente != null)
        //    {
        //        produtoExistente.AlterarNome(produto.Nome);
        //        produtoExistente.AlterarDescricao(produto.Descricao);
        //        EscreverProdutosNoArquivo(produtos);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    throw new NotImplementedException();
        //}

        public async Task Atualizar(Produto produto)
        {
            var buscaProduto = _produtoRepository.FilterBy(filter => filter.Codigo == produto.Codigo);
            var produtoAtualizar = buscaProduto.FirstOrDefault();

            if (produtoAtualizar == null)
            {
                throw new ApplicationException("Produto não encontrado.");
            }

            produtoAtualizar.Nome = produto.Nome;
            produtoAtualizar.Descricao = produto.Descricao;
            produtoAtualizar.Ativo = produto.Ativo;
            produtoAtualizar.Valor = produto.Valor;
            produtoAtualizar.Estoque = produto.Estoque;

            await _produtoRepository.ReplaceOneAsync(_mapper.Map<ProdutoCollection>(produtoAtualizar));
        }


        //public bool Deletar(int id)
        //{
        //    List<Produto> produtos = LerProdutosDoArquivo();
        //    var produtoExistente = produtos.FirstOrDefault(p => p.Codigo == id);
        //    if (produtoExistente != null)
        //    {
        //        produtos.Remove(produtoExistente);
        //        EscreverProdutosNoArquivo(produtos);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    var buscaProduto = _produtoRepository.DeleteById();

        //    throw new NotImplementedException();
        //}

        public async Task Ativar(Produto produto)
        {
            var buscaProduto = _produtoRepository.FilterBy(filter => filter.Codigo == produto.Codigo);

            var produtoAtivar = buscaProduto.FirstOrDefault();

            produtoAtivar.Ativo = produto.Ativo;

            await _produtoRepository.ReplaceOneAsync(_mapper.Map<ProdutoCollection>(produtoAtivar));
        }

        public async Task Desativar(Produto produto)
        {
            var buscaProduto = _produtoRepository.FilterBy(filter => filter.Codigo == produto.Codigo);

            var produtoDesativar = buscaProduto.FirstOrDefault();

            produtoDesativar.Ativo = produto.Ativo;

            await _produtoRepository.ReplaceOneAsync(_mapper.Map<ProdutoCollection>(produtoDesativar));
        }

        public async Task AlterarPreco(Produto produto, decimal valor)
        {
            var buscaProduto = _produtoRepository.FilterBy(filter => filter.Codigo == produto.Codigo);

            var produtoPreco = buscaProduto.FirstOrDefault();

            produtoPreco.Valor = produto.Valor;

            await _produtoRepository.ReplaceOneAsync(_mapper.Map<ProdutoCollection>(produtoPreco));
        }

        public async Task AtualizarEstoque(Produto produto, int quantidade)
        {
            var buscaProduto = _produtoRepository.FilterBy(filter => filter.Codigo == produto.Codigo);

            var produtoEstoque = buscaProduto.FirstOrDefault();

            produtoEstoque.Estoque = produto.Estoque;

            await _produtoRepository.ReplaceOneAsync(_mapper.Map<ProdutoCollection>(produtoEstoque));
        }

        public async Task Deletar(Produto produto)
        {
            var buscaProduto = _produtoRepository.FilterBy(filter => filter.Codigo == produto.Codigo);
            var produtoAtualizar = buscaProduto.FirstOrDefault();

            if (produtoAtualizar == null)
            {
                throw new ApplicationException("Produto não encontrado.");
            }

            // Use o código do produto a ser excluído para criar o filtro
            var filtro = Builders<ProdutoCollection>.Filter.Eq(p => p.Codigo, produto.Codigo);

            // Chame o método DeleteOne com o filtro
            await _produtoRepository.DeleteOneAsync(filtro);
        }



        #endregion

        #region Métodos do Arquivo
        //private List<Produto> LerProdutosDoArquivo()
        //{
        //    if (!System.IO.File.Exists(_produtoCaminhoArquivo))
        //    {
        //        return new List<Produto>();
        //    }

        //    string json = System.IO.File.ReadAllText(_produtoCaminhoArquivo);
        //    return JsonConvert.DeserializeObject<List<Produto>>(json);
        //}

        //private int ObterProximoCodigoDisponivel()
        //{
        //    List<Produto> produtos = LerProdutosDoArquivo();
        //    if (produtos.Any())
        //    {
        //        return produtos.Max(p => p.Codigo) + 1;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}

        //private void EscreverProdutosNoArquivo(List<Produto> produtos)
        //{
        //    string json = JsonConvert.SerializeObject(produtos);
        //    System.IO.File.WriteAllText(_produtoCaminhoArquivo, json);
        //}
        #endregion
    }
}
