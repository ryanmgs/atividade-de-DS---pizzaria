using System;

namespace SistemaPizzaria
{
    public class ItemPedido
    {
        private Produto produto;
        private int quantidade;

        public Produto Produto
        {
            get { return produto; }
        }

        public int Quantidade
        {
            get { return quantidade; }
        }

        public decimal Subtotal
        {
            get { return produto.Preco * quantidade; }
        }

        public ItemPedido(Produto produto, int quantidade)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto));

            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            this.produto = produto;
            this.quantidade = quantidade;
        }

        public void AdicionarQuantidade(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            this.quantidade += quantidade;
        }
    }
}
