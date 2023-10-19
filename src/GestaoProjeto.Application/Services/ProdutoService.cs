using AutoMapper;
using GestaoProduto.Domain.Entities;
using GestaoProduto.Domain.Interfaces;
using GestaoProjeto.Application.Interfaces;
using GestaoProjeto.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        #region Construtores
        private readonly IProdutoRepository _produtoRepository;
        private IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }
        #endregion

        #region Funções
        public void Adicionar(NovoProdutoViewModel novoProdutoViewModel)
        {
            var novoProduto = _mapper.Map<Produto>(novoProdutoViewModel);
            _produtoRepository.Adicionar(novoProduto);
        }

        public async Task AlterarPreco(int id, decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor não pode ser zerado ou negativo.");
            }

            var buscaProduto = await _produtoRepository.ObterPorId(id);

            if (buscaProduto == null)
            {
                throw new ApplicationException("Não é possível alterar o preço de um produto que não existe!");
            }

            buscaProduto.AlterarPreco(valor);

            await _produtoRepository.AlterarPreco(buscaProduto, valor);
        }


        public async Task Ativar(int id)
        {
            var buscaProduto = await _produtoRepository.ObterPorId(id);

            if (buscaProduto == null) throw new ApplicationException("Não é possível ativar um produto que não existe!");

            buscaProduto.Ativar();

            await _produtoRepository.Ativar(buscaProduto);
        }

        public async Task Atualizar(NovoProdutoViewModel novoprodutoViewModel)
        {
            var produto = _mapper.Map<Produto>(novoprodutoViewModel);        
            await _produtoRepository.Atualizar(produto);
        }

        public async Task AtualizarEstoque(int id, int quantidade)
        {
            var buscaProduto = await _produtoRepository.ObterPorId(id);

            if (buscaProduto == null)
            {
                throw new ApplicationException("Não é possível alterar o preço de um produto que não existe!");
            }

            if (buscaProduto.Estoque + quantidade < 0)
            {
                throw new ArgumentException("O estoque não pode ser negativo.");
            }

            buscaProduto.AtualizarEstoque(quantidade);

            await _produtoRepository.AtualizarEstoque(buscaProduto, quantidade);
        }

        //public async Task Delete(int id)
        //{
        //    var produto = await _produtoRepository.ObterPorId(id);

        //    if (produto == null)
        //    {
        //        throw new ApplicationException("Produto não encontrado.");
        //    }

        //    _produtoRepository.DeleteById(produto.Id); // Supondo que você tenha uma propriedade 'Id' na sua classe 'Produto' ou 'ProdutoCollection'
        //}



        public async Task Desativar(int id)
        {
            var buscaProduto = await _produtoRepository.ObterPorId(id);

            if (buscaProduto == null) throw new ApplicationException("Não é possível desativar um produto que não existe!");

            buscaProduto.Desativar();

            await _produtoRepository.Desativar(buscaProduto);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorNome(string produtoNome)
        {
            if (string.IsNullOrWhiteSpace(produtoNome))
            {
                return Enumerable.Empty<ProdutoViewModel>();
            }

            var produtos = await _produtoRepository.ObterPorNome(produtoNome);

            var produtosViewModel = _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);

            return produtosViewModel;
        }

        public async Task<ProdutoViewModel> ObterPorId(int id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            return _mapper.Map<ProdutoViewModel>(produto);
        }

        public IEnumerable<ProdutoViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(_produtoRepository.ObterTodos());
        }
        #endregion
    }
}