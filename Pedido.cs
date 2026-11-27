using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaPizzaria
{
    public class Pedido
    {
        private readonly List<ItemPedido> itens;
        private bool finalizado;

        public Cliente Cliente { get; private set; }

        public IReadOnlyList<ItemPedido> Itens
        {
            get { return itens.AsReadOnly(); }
        }

        public bool Finalizado
        {
            get { return finalizado; }
        }

        public bool EstaVazio
        {
            get { return itens.Count == 0; }
        }

        public decimal Total
        {
            get { return itens.Sum(item => item.Subtotal); }
        }

        public Pedido(Cliente cliente)
        {
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            itens = new List<ItemPedido>();
            finalizado = false;
        }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (finalizado)
                throw new InvalidOperationException("Este pedido já foi finalizado.");

            if (produto == null)
                throw new ArgumentNullException(nameof(produto));

            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            
            ItemPedido existente = itens.FirstOrDefault(
                item => item.Produto.Id == produto.Id
            );

            if (existente != null)
                existente.AdicionarQuantidade(quantidade);
            else
                itens.Add(new ItemPedido(produto, quantidade));
        }

        public void Finalizar()
        {
            if (EstaVazio)
                throw new InvalidOperationException("Não é possível finalizar um pedido vazio.");

            finalizado = true;
        }
    }
}
