using Microsoft.AspNetCore.Mvc;
using FichaCadastral.Models;
using LojaVirtual.Services;
using Microsoft.Extensions.Configuration;
using LojaVirtual.Models;
using System;

namespace FichaCadastral.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("login")]
        [HttpPost]
        public LoginResult Login(LoginRequest request)
        {
            var result = new LoginResult();

            if (request == null)
            {
                result.Sucesso = false;
                result.Mensagem = "Parânetro(s) inválido(s)";
            }
            else if (request.Email == "")
            {
                result.Sucesso = false;
                result.Mensagem = "O campo E-mail é obrigatório!";
            }
            else if (request.Senha == "")
            {
                result.Sucesso = false;
                result.Mensagem = "O campo Senha é obrigatório!";
            }
            else
            {
                var conectionString = _configuration.GetConnectionString("programacaoDoZeroDB");

                var usuarioService = new UsuarioService(conectionString);
                result = usuarioService.Login(request.Email, request.Senha);
            }
            
            return result;
        }

        [Route("cadastro")]
        [HttpPost]
        public CadastroResult Cadastro(CadastroRequest request)
        {
            var result = new CadastroResult();

            if (request == null ||
                string.IsNullOrWhiteSpace(request.Nome) ||
                string.IsNullOrWhiteSpace(request.Sobrenome) ||
                string.IsNullOrWhiteSpace(request.Telefone) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Genero) ||
                string.IsNullOrWhiteSpace(request.Senha))
            {
                result.Sucesso = false;
                result.Mensagem = "Todos os campos são obrigatórios!";
            }
            else
            {
                var conectionString = _configuration.GetConnectionString("programacaoDoZeroDB");

                var usuarioService = new UsuarioService(conectionString);

                result = usuarioService.Cadastro(request.Nome, request.Sobrenome, request.Telefone, request.Email, request.Genero, request.Senha);
            }
                        
            return result;
        }

        [Route("esqueceuSenha")]
        [HttpPost]
        public EsqueceuSenhaResult EsqueceuSenha(EsqueceuSenhaRequest request)
        {
            var result = new EsqueceuSenhaResult();

            if (request == null || string.IsNullOrEmpty(request.Email))
            {
                result.Sucesso = false;
                result.Mensagem = "O campo E-mail é obrigatório!";
            }
            else
            {
                var conectionString = _configuration.GetConnectionString("programacaoDoZeroDB");

                var usuarioService = new UsuarioService(conectionString);
                result = usuarioService.EsqueceuSenha(request.Email);

                result.Sucesso = true;
                result.Mensagem = "E-mail de recuperação enviado com sucesso!";
            }
            
            return result;
        }

        [Route("obterUsuario")]
        [HttpGet]
        public ObterUsuarioResult ObterUsuario(Guid usuarioGuid)
        {
            var result = new ObterUsuarioResult();

            if (usuarioGuid == null)
            {
                result.Mensagem = "Usuário Guid não encontrado.";
            }
            else
            {
                var conectionString = _configuration.GetConnectionString("programacaoDoZeroDB");
                var usuario = new UsuarioService(conectionString).ObterUsuario(usuarioGuid);

                if (usuario == null)
                {
                    result.Mensagem = "Usuário não existe.";
                }
                else
                {
                    result.Sucesso = true;
                    result.Nome = usuario.Nome;
                }
            }

            return result;
        }
    }
}
