using System.Globalization;

namespace SistemaPizzaria
{

    public abstract class Produto
    {
        private int id;
        private string nome;
        private decimal preco;

        public int Id
        {
            get { return id; }
        }

        public string Nome
        {
            get { return nome; }
        }

        public decimal Preco
        {
            get { return preco; }
        }

        public abstract string Categoria { get; }

        protected Produto(int id, string nome, decimal preco)
        {
            this.id = id;
            this.nome = nome;
            this.preco = preco;
        }

        public string PrecoFormatado()
        {
            return Preco.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));
        }

        public abstract string ObterDescricao();
    }
}
