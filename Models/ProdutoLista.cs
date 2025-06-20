namespace TCM_Supermercado1.Models
{
    public class ProdutoLista
    {
        public int cod_produto { get; set; }
        public String? nome_categoria { get; set; }
        public String? nome_fornecedor { get; set; }
        public String? nome_produto { get; set; }
        public double? preco_produto { get; set; }
        public String? descricao_produto { get; set; }
        public int quantidade_produto { get; set; }
    }
}
}
