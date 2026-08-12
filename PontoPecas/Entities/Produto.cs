namespace PontoPecas.Entities
{
    public class Produto
    {
        public int Id { get; set; }

        public string Sku { get; set; } = string.Empty;

        public string Nome { get; set; } = string.Empty;

        public decimal PrecoEntrada { get; set; }

        public decimal PrecoSaida { get; set; }

        public int QuantidadeEstoque { get; set; }

        public string? Observacao { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; } = null!;
    }
}
