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
    public class CategoriaService : ICategoriaService
    {
        #region Construtores
        private readonly ICategoriaRepository _categoriaRepository;
        private IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }
        #endregion

        #region Funções
        public void Adicionar(NovaCategoriaViewModel novaCategoriaViewModel)
        {
            var novaCategoria = _mapper.Map<Categoria>(novaCategoriaViewModel);
            _categoriaRepository.Adicionar(novaCategoria);
        }

        public bool Atualizar(NovaCategoriaViewModel novaCategoriaViewModel)
        {
            var categoria = _mapper.Map<Categoria>(novaCategoriaViewModel);
            bool atualizadoComSucesso = _categoriaRepository.Atualizar(categoria);

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
            bool excluidoComSucesso = _categoriaRepository.Deletar(id);

            if (excluidoComSucesso)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<IEnumerable<CategoriaViewModel>> ObterPorCategoria(int codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoriaViewModel> ObterPorId(int id)
        {
            var categorias = await _categoriaRepository.ObterPorId(id);
            return _mapper.Map<CategoriaViewModel>(categorias);
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterTodos()
        {
            var categorias = await _categoriaRepository.ObterTodos();
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(categorias);
        }
        #endregion
    }
}
