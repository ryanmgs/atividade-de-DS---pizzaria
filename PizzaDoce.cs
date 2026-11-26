namespace SistemaPizzaria
{
    // Herança: PizzaDoce também herda os dados e comportamentos de Produto.
    public class PizzaDoce : Produto
    {
        private string ingredientes;

        public override string Categoria
        {
            get { return "Doce"; }
        }

        public PizzaDoce(int id, string nome, decimal preco, string ingredientes)
            : base(id, nome, preco)
        {
            this.ingredientes = ingredientes;
        }

        // Polimorfismo: implementação específica da descrição da pizza doce.
        public override string ObterDescricao()
        {
            return $"{Nome} | {Categoria} | Ingredientes: {ingredientes} | Preço: {PrecoFormatado()}";
        }
    }
}
