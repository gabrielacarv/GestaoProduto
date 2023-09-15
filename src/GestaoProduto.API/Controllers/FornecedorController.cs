using GestaoProduto.Domain.Entities;
using GestaoProduto.Domain.Interfaces;
using GestaoProjeto.Application.Interfaces;
using GestaoProjeto.Application.Services;
using GestaoProjeto.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProduto.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedorController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fornecedores = await _fornecedorService.ObterTodos();
            return Ok(fornecedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var fornecedor = await _fornecedorService.ObterPorId(id);
            return Ok(fornecedor);
        }

        [HttpPost]
        public IActionResult Post(NovoFornecedorViewModel novoFornecedorViewModel)
        {
            _fornecedorService.Adicionar(novoFornecedorViewModel);
            return Ok("Registro adicionado com sucesso!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, NovoFornecedorViewModel novoFornecedorViewModel)
        {
            novoFornecedorViewModel.Codigo = id;
            bool atualizadoComSucesso = _fornecedorService.Atualizar(novoFornecedorViewModel);

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
            bool excluidoComSucesso = _fornecedorService.Deletar(id);

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
