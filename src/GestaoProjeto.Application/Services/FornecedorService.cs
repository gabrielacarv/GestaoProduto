using AutoMapper;
using GestaoProduto.Domain.Entities;
using GestaoProduto.Domain.Interfaces;
using GestaoProjeto.Application.Interfaces;
using GestaoProjeto.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        #region Construtores
        private readonly IFornecedorRepository _fornecedorRepository;
        private IMapper _mapper;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }
        #endregion


        #region Funções
        public async Task Adicionar(NovoFornecedorViewModel novoFornecedorViewModel)
        {
            var novoFornecedor =  _mapper.Map<Fornecedor>(novoFornecedorViewModel);
            await _fornecedorRepository.Adicionar(novoFornecedor);
        }

        public async Task AlterarEmailContato(int id, string novoEmail)
        {
            var buscaFornecedor = await _fornecedorRepository.ObterPorId(id);

            if (buscaFornecedor == null)
            {
                throw new ApplicationException("Não é possível alterar o email de um fornecedor que não existe!");
            }

            buscaFornecedor.AlterarEmailContato(novoEmail);

            await _fornecedorRepository.AlterarEmailContato(buscaFornecedor, novoEmail);
        }

        public async Task AlterarRazaoSocial(int id, string novaRazaoSocial)
        {
            var buscaFornecedor = await _fornecedorRepository.ObterPorId(id);

            if (buscaFornecedor == null)
            {
                throw new ApplicationException("Não é possível alterar a razão social de um fornecedor que não existe!");
            }

            buscaFornecedor.AlterarRazaoSocial(novaRazaoSocial);

            await _fornecedorRepository.AlterarRazaoSocial(buscaFornecedor, novaRazaoSocial);
        }

        //public bool Atualizar(NovoFornecedorViewModel fornecedorViewModel)
        //{
        //    var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
        //    bool atualizadoComSucesso = _fornecedorRepository.Atualizar(fornecedor);

        //    if (atualizadoComSucesso)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public async Task Atualizar(NovoFornecedorViewModel novoFornecedorViewModel)
        {
            var fornecedor = _mapper.Map<Fornecedor>(novoFornecedorViewModel);
            await _fornecedorRepository.Atualizar(fornecedor);
        }

        public bool Deletar(int id)
        {
            bool excluidoComSucesso = _fornecedorRepository.Deletar(id);

            if (excluidoComSucesso)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<FornecedorViewModel>> ObterPorFornecedor(string nomeFornecedor)
        {
            if (string.IsNullOrWhiteSpace(nomeFornecedor))
            {
                return Enumerable.Empty<FornecedorViewModel>();
            }

            var fornecedores = await _fornecedorRepository.ObterPorFornecedor(nomeFornecedor);

            var fornecedoresViewModel = _mapper.Map<IEnumerable<FornecedorViewModel>>(fornecedores);

            return fornecedoresViewModel;
        }

        public async Task<FornecedorViewModel> ObterPorId(int id)
        {
            var fornecedores = await _fornecedorRepository.ObterPorId(id);
            return _mapper.Map<FornecedorViewModel>(fornecedores);
        }

        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            //var fornecedores = await _fornecedorRepository.ObterTodos();
            //return _mapper.Map<IEnumerable<FornecedorViewModel>>(fornecedores);
            return _mapper.Map<IEnumerable<FornecedorViewModel>>(_fornecedorRepository.ObterTodos());
        }

        public async Task Ativar(int id)
        {
            var buscaFornecedor = await _fornecedorRepository.ObterPorId(id);

            if (buscaFornecedor == null) 
                throw new ApplicationException("Não é possível ativar um fornecedor que não existe!");

            buscaFornecedor.Ativar();

            await _fornecedorRepository.Ativar(buscaFornecedor);
        }

        public async Task Desativar(int id)
        {
            var buscaFornecedor = await _fornecedorRepository.ObterPorId(id);

            if (buscaFornecedor == null) 
                throw new ApplicationException("Não é possível desativar um fornecedor que não existe!");

            buscaFornecedor.Desativar();

            await _fornecedorRepository.Desativar(buscaFornecedor);
        }
        #endregion
    }
}
