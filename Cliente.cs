using System.Linq;

namespace SistemaPizzaria
{
    // Encapsulamento: os dados do cliente ficam protegidos.
    public class Cliente
    {
        private string nome;
        private string cpf;

        public string Nome
        {
            get { return nome; }
        }

        public string Cpf
        {
            get { return cpf; }
        }

        public Cliente(string nome, string cpf)
        {
            this.nome = nome;
            this.cpf = cpf;
        }

        public static bool ValidarCpf(string cpf)
        {
            string numeros = new string(cpf.Where(char.IsDigit).ToArray());

            if (numeros.Length != 11)
                return false;

            if (numeros.All(c => c == numeros[0]))
                return false;

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (numeros[i] - '0') * (10 - i);

            int primeiroDigito = (soma * 10) % 11;

            if (primeiroDigito == 10)
                primeiroDigito = 0;

            if (primeiroDigito != numeros[9] - '0')
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (numeros[i] - '0') * (11 - i);

            int segundoDigito = (soma * 10) % 11;

            if (segundoDigito == 10)
                segundoDigito = 0;

            return segundoDigito == numeros[10] - '0';
        }
    }
}
