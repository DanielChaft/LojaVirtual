using FichaCadastral.Models;
using LojaVirtual.Common;
using LojaVirtual.Entities;
using LojaVirtual.Repositories;
using System;

namespace LojaVirtual.Services
{
    public class UsuarioService
    {
        private readonly string _conectionString;

        public UsuarioService(string conectionString)
        {
            _conectionString = conectionString;
        }

        public LoginResult Login(string email, string senha)
        {
            var result = new LoginResult();

            var usuarioRepository = new UsuarioRepository(_conectionString);

            var usuario = usuarioRepository.ObterUsuarioPorEmail(email);

            if (usuario == null)
            {
                //usuário não existe
                result.Sucesso = false;
                result.Mensagem = "Usuário ou senha inválido.";
            }
            else
            {
                //usuário encontrado
                if (usuario.Senha == senha)
                {
                    //senha válida efetua o login
                    result.Sucesso = true;
                    result.UsuarioGuid = usuario.UsuarioGuid;
                }
                else
                {
                    //senha inválida
                    result.Sucesso = false;
                    result.Mensagem = "Usuário ou senha inválido.";
                }
            }
            
            return result;
        }

        public CadastroResult Cadastro(string nome, string sobrenome, string telefone, string email, string genero, string senha)
        {
            var result = new CadastroResult();

            var usuarioRepository = new UsuarioRepository(_conectionString);

            var usuario = usuarioRepository.ObterUsuarioPorEmail(email);

            if (usuario != null)
            {
                //usuario já existe
                result.Sucesso = false;
                result.Mensagem = "Usuário já existe no sistema.";
            }
            else
            {
                //usuario não existe
                usuario = new Usuario
                {
                    Nome = nome,
                    Sobrenome = sobrenome,
                    Telefone = telefone,
                    Email = email,
                    Genero = genero,
                    Senha = senha,
                    UsuarioGuid = Guid.NewGuid()
                };

                var affectedRows = usuarioRepository.Inserir(usuario);

                if (affectedRows > 0)
                {
                    //inseriu com sucesso
                    result.Sucesso = true;
                    result.UsuarioGuid = usuario.UsuarioGuid;
                    result.Mensagem = "Usuário cadastrado com sucesso!";
                }
                else
                {
                    //erro ao inserir
                    result.Sucesso = false;
                    result.Mensagem = "Não foi possível inserir o usuário. Tente novamente.";
                }
                                
            }

            return result;
        }

        public EsqueceuSenhaResult EsqueceuSenha(string email)
        {
            var result = new EsqueceuSenhaResult();

            Usuario usuario = new UsuarioRepository(_conectionString).ObterUsuarioPorEmail(email);
            
            if (usuario == null)
            {
                //usuario não existe
                result.Sucesso = false;
                result.Mensagem = "Usuário não existe para este e-mail.";
            }
            else
            {
                //usuário existente
                var emailSender = new EmailSender();
                var assunto = "PROG Z - E-mail de recuperação de senha.";
                var corpo = "Sua senha atual é: " + usuario.Senha;

                emailSender.Enviar(assunto, corpo, usuario.Email);
            }

            return result;
        }

        public Usuario ObterUsuario(Guid usuarioGuid)
        {
            Usuario usuario = new UsuarioRepository(_conectionString).ObterPorGuid(usuarioGuid);
            return usuario;
        }
    }
}
