using FichaCadastral.Models;
using LojaVirtual.Entities;
using LojaVirtual.Repositories;

namespace LojaVirtual.Services
{
    public class ProdutoService
    {
        private readonly string _conectionString;

        public ProdutoService(string conectionString)
        {
            _conectionString = conectionString;
        }
        public ProdutoResult Cadastro(string codigo, string nome, string preco, string quantidade)
        {
            var result = new ProdutoResult();

            var produtoRepository = new ProdutoRepository(_conectionString);

            var produto = produtoRepository.ObterProdutoPorCodigo(codigo);

            if (produto != null)
            {
                //produto já existe
                result.Sucesso = false;
                result.Mensagem = "Produto com código já cadastrado no sistema.";
            }
            else
            {
                //produto não existe
                produto = new Produto
                {
                    Codigo = codigo,
                    Nome = nome,
                    Preco = preco,
                    Quantidade = quantidade,
                };

                var affectedRows = produtoRepository.Inserir(produto);

                if (affectedRows > 0)
                {
                    //inseriu com sucesso
                    result.Sucesso = true;
                    result.Codigo = produto.Codigo;
                    result.Mensagem = "Produto cadastrado com sucesso!";
                }
                else
                {
                    //erro ao inserir
                    result.Sucesso = false;
                    result.Mensagem = "Não foi possível cadastrar o produto. Tente novamente.";
                }
            }
            return result;
        }
    }
}
