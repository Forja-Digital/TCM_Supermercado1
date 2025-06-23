namespace TCM_Supermercado1.Models
{
    public class PedidoLista
    {
        public int cod_pedido { get; set; }
        public string nome_funcionario { get; set; }
        public string nome_cliente { get; set; }
        public string tipo_pagamento { get; set; }
        public DateTime data_pedido { get; set; }
        public double total { get; set; }
        public string estado_uf { get; set; }
        public string endereco_pedido { get; set; }
        public string complemento_pedido { get; set; }
    }
}
