namespace TCM_Supermercado1.Models
{
    public class Pedido
    {
            public int cod_pedido { get; set; }
            public int cod_funcionario { get; set; }
            public int cod_pagamento { get; set; }     
            public int cod_cliente { get; set; }
            public DateTime data_pedido { get; set; }
            public double? total { get; set; }
            public int cod_estado { get; set; }
            public String? endereco_pedido { get; set; }
            public String? complemento_pedido { get; set; }

            public List<ItemPedido> itens { get; set; } = new List<ItemPedido>();

    }
}
