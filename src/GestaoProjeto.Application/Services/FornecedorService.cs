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
        public void Adicionar(NovoFornecedorViewModel novoFornecedorViewModel)
        {
            var novoFornecedor = _mapper.Map<Fornecedor>(novoFornecedorViewModel);
            _fornecedorRepository.Adicionar(novoFornecedor);
        }

        public bool Atualizar(NovoFornecedorViewModel fornecedorViewModel)
        {
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            bool atualizadoComSucesso = _fornecedorRepository.Atualizar(fornecedor);

            if (atualizadoComSucesso)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public Task<IEnumerable<FornecedorViewModel>> ObterPorCategoria(int codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<FornecedorViewModel> ObterPorId(int id)
        {
            var fornecedores = await _fornecedorRepository.ObterPorId(id);
            return _mapper.Map<FornecedorViewModel>(fornecedores);
        }

        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            var fornecedores = await _fornecedorRepository.ObterTodos();
            return _mapper.Map<IEnumerable<FornecedorViewModel>>(fornecedores);
        }
        #endregion
    }
}
