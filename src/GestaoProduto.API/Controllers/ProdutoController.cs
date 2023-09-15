using GestaoProjeto.Application.Interfaces;
using GestaoProjeto.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProduto.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var produtos = await _produtoService.ObterTodos();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var produto = await _produtoService.ObterPorId(id);
            return Ok(produto);
        }

        [HttpPost]
        public IActionResult Post(NovoProdutoViewModel novoProdutoViewModel)
        {
            _produtoService.Adicionar(novoProdutoViewModel);
            return Ok("Registro adicionado com sucesso!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, NovoProdutoViewModel novoProdutoViewModel)
        {
            novoProdutoViewModel.Codigo = id;         
            bool atualizadoComSucesso = _produtoService.Atualizar(novoProdutoViewModel);

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
            bool excluidoComSucesso = _produtoService.Deletar(id);

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
