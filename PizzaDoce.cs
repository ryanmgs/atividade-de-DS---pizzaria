namespace SistemaPizzaria
{
    // Herança: PizzaDoce herda de Produto.
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

        
        public override string ObterDescricao()
        {
            return $"{Nome} | {Categoria} | Ingredientes: {ingredientes} | Preço: {PrecoFormatado()}";
        }
    }
}
