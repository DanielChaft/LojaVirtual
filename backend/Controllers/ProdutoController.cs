using Microsoft.AspNetCore.Mvc;
using FichaCadastral.Models;
using LojaVirtual.Services;
using Microsoft.Extensions.Configuration;
using LojaVirtual.Models;

namespace FichaCadastral.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProdutoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("cadastro")]
        [HttpPost]
        public ProdutoResult Cadastro(ProdutoRequest request)
        {
            var result = new ProdutoResult();

            if (request == null ||
                string.IsNullOrWhiteSpace(request.Codigo) ||
                string.IsNullOrWhiteSpace(request.Nome) ||
                string.IsNullOrWhiteSpace(request.Preco) ||
                string.IsNullOrWhiteSpace(request.Quantidade))
            {
                result.Sucesso = false;
                result.Mensagem = "Todos os campos são obrigatórios!";
            }
            else
            {
                var conectionString = _configuration.GetConnectionString("programacaoDoZeroDB");

                var produtoService = new ProdutoService(conectionString);

                result = produtoService.Cadastro(request.Codigo, request.Nome, request.Preco, request.Quantidade);
            }
                        
            return result;
        }
    }
}
