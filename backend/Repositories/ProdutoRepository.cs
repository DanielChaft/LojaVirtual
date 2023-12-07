using MySql.Data.MySqlClient;
using LojaVirtual.Entities;

namespace LojaVirtual.Repositories
{
    public class ProdutoRepository
    {
        private readonly string _conectionString;

        public ProdutoRepository(string conectionString)
        {
            _conectionString = conectionString;
        }

        public int Inserir(Produto produto)
        {
            var connection = new MySqlConnection(_conectionString);
            var cmd = new MySqlCommand
            {
                Connection = connection,
                CommandText = "INSERT INTO produto (codigo, nome, preco, quantidade) VALUES (@codigo, @nome, @preco, @quantidade)"
            };

            cmd.Parameters.AddWithValue("codigo", produto.Codigo);
            cmd.Parameters.AddWithValue("nome", produto.Nome);
            cmd.Parameters.AddWithValue("preco", produto.Preco);
            cmd.Parameters.AddWithValue("quantidade", produto.Quantidade);

            connection.Open();

            var affectedRows = cmd.ExecuteNonQuery();

            connection.Close();

            return affectedRows;
        }

        public Produto ObterProdutoPorCodigo(string codigo)
        {
            Produto produto = null;

            MySqlConnection connection = new MySqlConnection(_conectionString);

            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection,

                CommandText = "SELECT * FROM produto WHERE codigo = @codigo"
            };

            cmd.Parameters.AddWithValue("codigo", codigo);

            connection.Open();

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                produto = new Produto
                {
                    Codigo = reader["codigo"].ToString(),
                    Nome = reader["Nome"].ToString(),
                    Preco = reader["preco"].ToString(),
                    Quantidade = reader["quantidade"].ToString(),
                };
            }

            connection.Close();

            return produto;
        }
    }
}
