using Crawler.Scraps;
using Crawler.Send;
using System;

namespace Crawler.Compare
{
    public class Benchmark
    {
        public void CompararPrecos(string nomeProduto, ProdutoScraper precoMagazineLuiza, ProdutoScraper precoMercadoLivre)
        {
            // Remover caracteres extras e espaços em branco
            var cleanPrecoMagazineLuiza = RemoveExtraCharacters(precoMagazineLuiza.Price);
            var cleanPrecoMercadoLivre = RemoveExtraCharacters(precoMercadoLivre.Price);

            // Convertendo os preços para decimal
            if (decimal.TryParse(cleanPrecoMagazineLuiza, out decimal precoMagazine) && decimal.TryParse(cleanPrecoMercadoLivre, out decimal precoMercado))
            {
                string MelhorCompra;
                string UrlMelhorCompra;

                // Sua lógica de comparação de preços aqui
                if (precoMagazine < precoMercado)
                {
                    // Exibindo os preços
                    Console.WriteLine($"Produto: {nomeProduto}");
                    Console.WriteLine($"Preço no Magazine Luiza: R${precoMagazine}");
                    Console.WriteLine($"Preço no Mercado Livre: R${precoMercado}");
                    Console.WriteLine("O preço no Magazine Luiza é mais barato.");
                    MelhorCompra = "Magazine Luiza";
                    UrlMelhorCompra = precoMagazineLuiza.Url;
                    Email.EnviarEmail(nomeProduto, precoMercadoLivre.Price, nomeProduto, precoMagazineLuiza.Price, MelhorCompra, UrlMelhorCompra, email : Program.emailInformado);
                }
                else if (precoMagazine > precoMercado)
                {
                    // Exibindo os preços
                    Console.WriteLine($"Produto: {nomeProduto}");
                    Console.WriteLine($"Preço no Magazine Luiza: R${precoMagazine}");
                    Console.WriteLine($"Preço no Mercado Livre: R${precoMercado}");
                    Console.WriteLine("O preço no Mercado Livre é mais barato.");
                    MelhorCompra = "Mercado Livre";
                    UrlMelhorCompra = precoMercadoLivre.Url;
                    Email.EnviarEmail(nomeProduto, precoMagazineLuiza.Price, nomeProduto, precoMercadoLivre.Price, MelhorCompra, UrlMelhorCompra, email : Program.emailInformado);
                }
                else
                {
                    // Exibindo os preços
                    Console.WriteLine($"Produto: {nomeProduto}");
                    Console.WriteLine($"Preço no Magazine Luiza: R${precoMagazine}");
                    Console.WriteLine($"Preço no Mercado Livre: R${precoMercado}");
                    Console.WriteLine("Os preços são iguais.");
                }
            }
            else
            {
                Console.WriteLine("Erro ao converter os preços para decimal.");
            }
        }

        // Método para remover caracteres extras
        private string RemoveExtraCharacters(string Price)
        {
            // Remover "R" e "$"
            return Price.Replace("R", "").Replace("$", "").Trim();
        }
    }
}
