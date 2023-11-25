using AutoMapper;
using GestaoProduto.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProduto.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

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
    }
}
