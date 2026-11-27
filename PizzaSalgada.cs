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


        public override string ObterDescricao()
        {
            return $"{Nome} | {Categoria} | Ingredientes: {ingredientes} | Preço: {PrecoFormatado()}";
        }
    }
}
