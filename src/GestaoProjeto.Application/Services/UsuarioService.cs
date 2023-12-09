using AutoMapper;
using GestaoProduto.Domain.Entities;
using GestaoProduto.Domain.Interfaces;
using GestaoProduto.Infra.Autenticacao;
using GestaoProduto.Infra.Autenticacao.Models;
using GestaoProjeto.Application.Interfaces;
using GestaoProjeto.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;


        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<string> Autenticar(AutenticarUsuarioViewModel autenticarUsuarioViewModel)
        {
            var usuario = await _usuarioRepository.Autenticar(autenticarUsuarioViewModel.Login, autenticarUsuarioViewModel.Senha);

            if (usuario == null)
                throw new ApplicationException("Login/Senha inválidos ou não existe");

            TokenRequest tokenRequest = new TokenRequest()
            {
                usuario = autenticarUsuarioViewModel.Login
            };

            string token = await _tokenService.GerarTokenJWT(tokenRequest);

            return token;

        }

        public async Task<string> Autenticar2(AutenticarUsuarioViewModel autenticarUsuarioViewModel)
        {
            var usuario = await _usuarioRepository.Autenticar2(autenticarUsuarioViewModel.Login);


            if (usuario == null)
                throw new ApplicationException("Login/Senha inválidos ou não existe");

            if (!VerificarSenha(autenticarUsuarioViewModel.Senha, usuario.Senha))
            {
                throw new ApplicationException("Login/Senha inválidos ou não existe");
            }

            TokenRequest tokenRequest = new TokenRequest()
            {
                usuario = autenticarUsuarioViewModel.Login
            };

            string token = await _tokenService.GerarTokenJWT(tokenRequest);

            return token;

        }

        public static bool VerificarSenha(string senhaDigitada, string hashArmazenado)
        {
            byte[] hashComSalt = Convert.FromBase64String(hashArmazenado);

            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashComSalt, hashComSalt.Length - 16, salt, 0, 16);

            byte[] senhaBytes = Encoding.UTF8.GetBytes(senhaDigitada);
            byte[] senhaComSalt = new byte[senhaBytes.Length + salt.Length];
            Buffer.BlockCopy(senhaBytes, 0, senhaComSalt, 0, senhaBytes.Length);
            Buffer.BlockCopy(salt, 0, senhaComSalt, senhaBytes.Length, salt.Length);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(senhaComSalt);

                byte[] novoHashComSalt = new byte[hashBytes.Length + salt.Length];
                Buffer.BlockCopy(hashBytes, 0, novoHashComSalt, 0, hashBytes.Length);
                Buffer.BlockCopy(salt, 0, novoHashComSalt, hashBytes.Length, salt.Length);

                string novoHashBase64 = Convert.ToBase64String(novoHashComSalt);

                return string.Equals(hashArmazenado, novoHashBase64);
            }
        }


        public async Task Cadastrar(UsuarioViewModel usuarioViewModel)
        {
            var usuario = _mapper.Map<Usuario>(usuarioViewModel);

            string senhaCifrada = Criptografia.CriptografarSenha(usuarioViewModel.Senha);
            usuario.Senha = senhaCifrada;

            await _usuarioRepository.Cadastrar(usuario);
        }
    }
}
