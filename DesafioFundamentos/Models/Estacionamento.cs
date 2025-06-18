using System.Text.RegularExpressions; // Necessário para usar expressões regulares

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar (formato AAA-0000 ou AAA0A00 para Mercosul):");
            string placa = Console.ReadLine();
            placa = placa.ToUpper();


            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine("Placa inválida. Por favor, digite uma placa.");
                return; // Sai do método se a placa for vazia
            }

            // 1. Validação de formato da placa (ex: AAA-0000 ou AAA0A00 para Mercosul)
            // Expressão regular para placas antigas (AAA-0000) ou Mercosul (AAA0A00)
            string padraoPlacaAntiga = @"^[A-Z]{3}-\d{4}$";
            string padraoPlacaMercosul = @"^[A-Z]{3}\d[A-Z]\d{2}$";

            bool isPlacaAntiga = Regex.IsMatch(placa, padraoPlacaAntiga);
            bool isPlacaMercosul = Regex.IsMatch(placa, padraoPlacaMercosul);

            if (!isPlacaAntiga && !isPlacaMercosul)
            {
                Console.WriteLine("Formato de placa inválido. Utilize o formato AAA-0000 ou AAA0A00 (Mercosul).");
                return; // Sai do método se o formato for inválido
            }

            // 2. Validação de duplicidade
            if (veiculos.Any(x => x == placa))
            {
                Console.WriteLine($"O veículo com placa '{placa}' já está estacionado.");
            }
            else
            {
                veiculos.Add(placa);
                Console.WriteLine($"Veículo com placa '{placa}' adicionado com sucesso!");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            string placa = Console.ReadLine();
            placa = placa.ToUpper();

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine("Placa inválida. Por favor, digite uma placa.");
                return;
            }

            if (veiculos.Any(x => x == placa))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                if (int.TryParse(Console.ReadLine(), out int horas))
                {
                    decimal valorTotal = precoInicial + (precoPorHora * horas);

                    veiculos.RemoveAll(x => x.ToUpper() == placa);

                    Console.WriteLine($"O veículo '{placa}' foi removido e o preço total foi de: R$ {valorTotal:F2}");
                }
                else
                {
                    Console.WriteLine("Quantidade de horas inválida. Por favor, digite um número inteiro.");
                }
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente.");
            }
        }

        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                for (int i = 0; i < veiculos.Count; i++)
                {
                    Console.WriteLine($"Nº {i + 1}: {veiculos[i]}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}