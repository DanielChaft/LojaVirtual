using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Entities
{
    public class Produto
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Preco { get; set; }
        public string Quantidade { get; set; }

    }
}
