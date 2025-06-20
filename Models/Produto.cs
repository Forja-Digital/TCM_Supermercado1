namespace TCM_Supermercado1.Models
{
    public class Produto
    {
        public int cod_produto { get; set; } 
        public int cod_categoria { get; set; } 
        public String? cnpj { get; set; }
        public String? nome_produto { get; set; }
        public double? preco_produto { get; set; }
        public String? descricao_produto { get; set; }
        public int quantidade_produto { get; set; }
    }
}
