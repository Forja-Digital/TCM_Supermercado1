namespace TCM_Supermercado1.Models
{
    public class ItemPedido
    {
            public int cod_itempedido { get; set; }
            public int cod_pedido { get; set; }
            public int cod_produto { get; set; }
            public int quantidade_itempedido { get; set; }
            public double preco_unitario { get; set; }
            public double total_item { get; set; }
    }
}
