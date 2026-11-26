using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SistemaPizzaria
{
    internal class Program
    {
        private static readonly CultureInfo CulturaBrasileira =
            CultureInfo.GetCultureInfo("pt-BR");

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("====================================");
            Console.WriteLine("       BEM-VINDO À PIZZARIA!");
            Console.WriteLine("====================================");

            Cliente cliente = CadastrarCliente();
            Pedido pedido = new Pedido(cliente);
            List<Produto> cardapio = CriarCardapio();

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("           MENU PRINCIPAL");
                Console.WriteLine("====================================");
                Console.WriteLine($"Cliente: {cliente.Nome}");
                Console.WriteLine();
                Console.WriteLine("1 - Ver menu");
                Console.WriteLine("2 - Fazer pedido");
                Console.WriteLine("3 - Visualizar pedido");
                Console.WriteLine("4 - Finalizar pedido");
                Console.WriteLine("====================================");

                int opcao = LerInteiro("Escolha uma opção: ", 1, 4);
                Console.Clear();

                switch (opcao)
                {
                    case 1:
                        ExibirCardapio(cardapio);
                        Pausar();
                        break;

                    case 2:
                        FazerPedido(cardapio, pedido);
                        Pausar();
                        break;

                    case 3:
                        VisualizarPedido(pedido);
                        Pausar();
                        break;

                    case 4:
                        continuar = !FinalizarPedido(pedido);
                        Pausar();
                        break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Obrigado por escolher nossa pizzaria!");
            Console.WriteLine("Volte sempre!");
        }

        // Cadastro do cliente.
        private static Cliente CadastrarCliente()
        {
            Console.WriteLine();
            Console.WriteLine("             CADASTRO");
            Console.WriteLine("------------------------------------");

            string nome;

            do
            {
                Console.Write("Digite seu nome: ");
                nome = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(nome))
                    Console.WriteLine("Digite um nome válido.");
            }
            while (string.IsNullOrWhiteSpace(nome));

            string cpf;

            while (true)
            {
                Console.Write("Digite seu CPF: ");
                cpf = Console.ReadLine() ?? string.Empty;

                if (Cliente.ValidarCpf(cpf))
                {
                    cpf = new string(cpf.Where(char.IsDigit).ToArray());
                    break;
                }

                Console.WriteLine("CPF inválido! Tente novamente.");
            }

            Cliente cliente = new Cliente(nome.Trim(), cpf);

            Console.WriteLine();
            Console.WriteLine("Cadastro realizado com sucesso!");
            Console.WriteLine($"Bem-vindo(a), {cliente.Nome}!");

            return cliente;
        }

        // Criação do cardápio com pizzas doces e salgadas.
        private static List<Produto> CriarCardapio()
        {
            List<Produto> cardapio = new List<Produto>();

            cardapio.Add(new PizzaSalgada(
                1, "Mussarela", 39.90m,
                "Mussarela, molho de tomate e orégano"));

            cardapio.Add(new PizzaSalgada(
                2, "Calabresa", 42.90m,
                "Calabresa, cebola, mussarela e molho"));

            cardapio.Add(new PizzaSalgada(
                3, "Frango com Catupiry", 45.90m,
                "Frango desfiado, catupiry e mussarela"));

            cardapio.Add(new PizzaSalgada(
                4, "Portuguesa", 48.90m,
                "Presunto, ovos, cebola, ervilha e mussarela"));

            cardapio.Add(new PizzaDoce(
                5, "Chocolate", 42.90m,
                "Chocolate ao leite e granulado"));

            cardapio.Add(new PizzaDoce(
                6, "Banana com Canela", 39.90m,
                "Banana, açúcar e canela"));

            cardapio.Add(new PizzaDoce(
                7, "Romeu e Julieta", 44.90m,
                "Goiabada e queijo"));

            cardapio.Add(new PizzaDoce(
                8, "Morango com Chocolate", 47.90m,
                "Morango e chocolate ao leite"));

            return cardapio;
        }

        // Exibição do cardápio. O método ObterDescricao demonstra polimorfismo.
        private static void ExibirCardapio(List<Produto> cardapio)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("              CARDÁPIO");
            Console.WriteLine("====================================");

            Console.WriteLine();
            Console.WriteLine("          PIZZAS SALGADAS");
            Console.WriteLine("------------------------------------");

            foreach (Produto produto in cardapio)
            {
                if (produto.Categoria == "Salgada")
                    Console.WriteLine($"[{produto.Id}] {produto.ObterDescricao()}");
            }

            Console.WriteLine();
            Console.WriteLine("           PIZZAS DOCES");
            Console.WriteLine("------------------------------------");

            foreach (Produto produto in cardapio)
            {
                if (produto.Categoria == "Doce")
                    Console.WriteLine($"[{produto.Id}] {produto.ObterDescricao()}");
            }

            Console.WriteLine();
            Console.WriteLine("------------------------------------");
        }

        // Adiciona pizzas e quantidades ao pedido.
        private static void FazerPedido(List<Produto> cardapio, Pedido pedido)
        {
            if (pedido.Finalizado)
            {
                Console.WriteLine("Este pedido já foi finalizado.");
                return;
            }

            bool adicionando = true;

            while (adicionando)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("             FAZER PEDIDO");
                Console.WriteLine("====================================");

                ExibirCardapio(cardapio);

                Console.WriteLine();
                Console.WriteLine("Digite o ID da pizza desejada.");
                Console.WriteLine("Digite 0 para voltar ao menu principal.");

                int id = LerInteiro("ID da pizza: ", 0, 8);

                if (id == 0)
                    break;

                Produto produto = cardapio.FirstOrDefault(p => p.Id == id);

                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado.");
                    Pausar();
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine($"Pizza escolhida: {produto.Nome}");
                Console.WriteLine($"Preço unitário: {produto.Preco.ToString("C2", CulturaBrasileira)}");

                int quantidade = LerInteiro("Digite a quantidade: ", 1, 100);
                pedido.AdicionarItem(produto, quantidade);

                Console.WriteLine();
                Console.WriteLine("Pizza adicionada ao pedido com sucesso!");
                Console.WriteLine(
                    $"Subtotal: {(produto.Preco * quantidade).ToString("C2", CulturaBrasileira)}");

                Console.WriteLine();
                Console.WriteLine("Deseja adicionar mais alguma pizza?");

                string resposta = LerRespostaSimNao();

                if (resposta == "N")
                    adicionando = false;
            }
        }

        // Mostra todos os itens, quantidades, subtotais e total.
        private static void VisualizarPedido(Pedido pedido)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("          SEU PEDIDO");
            Console.WriteLine("====================================");
            Console.WriteLine($"Cliente: {pedido.Cliente.Nome}");
            Console.WriteLine($"CPF: {pedido.Cliente.Cpf}");
            Console.WriteLine("------------------------------------");

            if (pedido.EstaVazio)
            {
                Console.WriteLine("Você ainda não adicionou nenhuma pizza.");
                return;
            }

            foreach (ItemPedido item in pedido.Itens)
            {
                Console.WriteLine();
                Console.WriteLine($"Pizza: {item.Produto.Nome}");
                Console.WriteLine($"Quantidade: {item.Quantidade}");
                Console.WriteLine(
                    $"Valor unitário: {item.Produto.Preco.ToString("C2", CulturaBrasileira)}");
                Console.WriteLine(
                    $"Subtotal: {item.Subtotal.ToString("C2", CulturaBrasileira)}");
                Console.WriteLine("------------------------------------");
            }

            Console.WriteLine();
            Console.WriteLine($"TOTAL: {pedido.Total.ToString("C2", CulturaBrasileira)}");
            Console.WriteLine();
            Console.WriteLine(pedido.Finalizado ? "Status: FINALIZADO" : "Status: EM ABERTO");
        }

        // Confirma e finaliza o pedido.
        private static bool FinalizarPedido(Pedido pedido)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("         FINALIZAR PEDIDO");
            Console.WriteLine("====================================");

            if (pedido.EstaVazio)
            {
                Console.WriteLine("Você não possui nenhum item no pedido.");
                Console.WriteLine("Adicione uma pizza antes de finalizar.");
                return false;
            }

            if (pedido.Finalizado)
            {
                Console.WriteLine("Este pedido já foi finalizado.");
                return true;
            }

            VisualizarPedido(pedido);

            Console.WriteLine();
            Console.WriteLine("Deseja confirmar a finalização do pedido?");

            string resposta = LerRespostaSimNao();

            if (resposta == "S")
            {
                pedido.Finalizar();

                Console.WriteLine();
                Console.WriteLine("====================================");
                Console.WriteLine("       PEDIDO FINALIZADO!");
                Console.WriteLine("====================================");
                Console.WriteLine($"Valor total: {pedido.Total.ToString("C2", CulturaBrasileira)}");
                Console.WriteLine($"Cliente: {pedido.Cliente.Nome}");
                Console.WriteLine("Seu pedido foi registrado com sucesso!");

                return true;
            }

            Console.WriteLine();
            Console.WriteLine("Finalização cancelada. Seu pedido continua aberto.");
            return false;
        }

        // Lê um número inteiro dentro do intervalo permitido.
        private static int LerInteiro(string mensagem, int minimo, int maximo)
        {
            int valor;

            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(entrada, out valor) && valor >= minimo && valor <= maximo)
                    return valor;

                Console.WriteLine($"Digite um número entre {minimo} e {maximo}.");
            }
        }

        // Lê uma resposta de sim ou não.
        private static string LerRespostaSimNao()
        {
            while (true)
            {
                Console.Write("Digite S para sim ou N para não: ");
                string resposta = (Console.ReadLine() ?? string.Empty).Trim().ToUpperInvariant();

                if (resposta == "S" || resposta == "N")
                    return resposta;

                Console.WriteLine("Resposta inválida. Digite S ou N.");
            }
        }

        private static void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
