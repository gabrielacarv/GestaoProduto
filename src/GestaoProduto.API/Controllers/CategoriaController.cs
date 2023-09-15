using GestaoProjeto.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GestaoProduto.Data.Repository;
using GestaoProjeto.Application.Interfaces;
using GestaoProduto.Domain.Entities;

namespace GestaoProduto.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categorias = await _categoriaService.ObterTodos();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var categoria = await _categoriaService.ObterPorId(id);
            return Ok(categoria);
        }

        [HttpPost]
        public IActionResult Post(NovaCategoriaViewModel novoFornecedorViewModel)
        {
            _categoriaService.Adicionar(novoFornecedorViewModel);
            return Ok("Registro adicionado com sucesso!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, NovaCategoriaViewModel novaCategoriaViewModel)
        {
            novaCategoriaViewModel.Codigo = id;
            bool atualizadoComSucesso = _categoriaService.Atualizar(novaCategoriaViewModel);

            if (atualizadoComSucesso)
            {
                return Ok("Registro atualizado com sucesso!");
            }
            else
            {
                return NotFound("Registro inexistente ou não pôde ser atualizado.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool excluidoComSucesso = _categoriaService.Deletar(id);

            if (excluidoComSucesso)
            {
                return Ok("Registro excluído com sucesso!");
            }
            else
            {
                return NotFound("Registro não encontrado ou não pôde ser excluído.");
            }
        }
    }
}