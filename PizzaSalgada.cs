namespace SistemaPizzaria
{
    // Herança: PizzaSalgada herda de Produto
    public class PizzaSalgada : Produto
    {
        private string ingredientes;

        public override string Categoria
        {
            get { return "Salgada"; }
        }

        public PizzaSalgada(int id, string nome, decimal preco, string ingredientes)
            : base(id, nome, preco)
        {
            this.ingredientes = ingredientes;
        }

        // Polimorfismo: implementação específica da descrição da pizza salgada.
        public override string ObterDescricao()
        {
            return $"{Nome} | {Categoria} | Ingredientes: {ingredientes} | Preço: {PrecoFormatado()}";
        }
    }
}
