using AutoMapper;
using GestaoProduto.Domain.Interfaces;
using GestaoProduto.Infra.Autenticacao;
using GestaoProduto.Infra.Autenticacao.Models;
using GestaoProjeto.Application.Interfaces;
using GestaoProjeto.Application.Services;
using GestaoProjeto.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProduto.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        //private readonly IUsuarioRepository _usuarioRepository;
        //private IMapper _mapper;

        //public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        //{
        //    _usuarioRepository = usuarioRepository;
        //    _mapper = mapper;
        //}

        //[HttpPost]
        //[Route("login")]
        //public async Task<ActionResult<dynamic>> Autenticar([FromBody] UsuarioViewModel usuarioViewModel)
        //{
        //    var buscarUsuario = _usuarioRepository.Autenticar(_mapper.Map<Usuario>(usuarioViewModel));

        //    if (buscarUsuario == null)
        //    {
        //        return NotFound(new { message = "Usuário não existe e/ou senha inválida" });
        //    }

        //    var token = TokenService.GenerateToken(buscarUsuario);

        //    buscarUsuario = "";

        //    return new
        //    {
        //        usuario = buscarUsuario,
        //        token = token
        //    };
        //}

        private readonly ITokenService _tokenService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper,
             ITokenService tokenService)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Autenticar(AutenticarUsuarioViewModel autenticarUsuarioViewModel)
        {
            var token = await _usuarioService.Autenticar2(autenticarUsuarioViewModel);

            return Ok(token);
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(UsuarioViewModel usuarioViewModel)
        {
            await _usuarioService.Cadastrar(usuarioViewModel);

            return Ok();
        }
    }
}
