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

        //[HttpPut("{id}")]
        //public IActionResult Put(int id, NovoFornecedorViewModel novoFornecedorViewModel)
        //{
        //    novoFornecedorViewModel.Codigo = id;
        //    bool atualizadoComSucesso = _fornecedorService.Atualizar(novoFornecedorViewModel);

        //    if (atualizadoComSucesso)
        //    {
        //        return Ok("Registro atualizado com sucesso!");
        //    }
        //    else
        //    {
        //        return NotFound("Registro inexistente ou não pôde ser atualizado.");
        //    }
        //}

        [HttpPut("{id}")]
        public IActionResult Put(int id, NovoFornecedorViewModel novoFornecedorViewModel)
        {
            novoFornecedorViewModel.Codigo = id;
            _fornecedorService.Atualizar(novoFornecedorViewModel);

            return Ok("Registro atualizado com sucesso!");
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

        [HttpPut("AtualizarEmail/{id}/{novoEmail}")]
        public async Task<IActionResult> AlterarEmailContato(int id ,string novoEmail)
        {
            await _fornecedorService.AlterarEmailContato(id, novoEmail);

            return Ok("Email do fornecedor alterado com sucesso");
        }

        [HttpPut("AtualizarRazaoSocial/{id}/{novaRazaoSocial}")]
        public async Task<IActionResult> AlterarRazaoSocial(int id, string novaRazaoSocial)
        {
            await _fornecedorService.AlterarRazaoSocial(id, novaRazaoSocial);

            return Ok("Razão social do fornecedor alterado com sucesso");
        }

        [HttpPut]
        [Route("Ativar/{id}")]
        public async Task<IActionResult> Ativa(int id)
        {
            await _fornecedorService.Ativar(id);

            return Ok("Fornecedor ativado com sucesso");
        }

        [HttpPut]
        [Route("Desativar/{id}")]
        public async Task<IActionResult> Desativa(int id)
        {
            await _fornecedorService.Desativar(id);

            return Ok("Fornecedor desativado com sucesso");
        }
    }
}
