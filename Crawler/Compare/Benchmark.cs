using Crawler.Scraps;
using Crawler.Send;
using System;
using static Program;

namespace Crawler.Compare
{
    public class Benchmark
    {
        public void CompararPrecos(string nomeProduto, ProdutoScraper precoMagazineLuiza, ProdutoScraper precoMercadoLivre, string emailDestino)
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
                    Email.EnviarEmail(emailDestino, nomeProduto, precoMercadoLivre.Price, nomeProduto, precoMagazineLuiza.Price, MelhorCompra, UrlMelhorCompra);
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
                    Email.EnviarEmail(emailDestino, nomeProduto, precoMagazineLuiza.Price, nomeProduto, precoMercadoLivre.Price, MelhorCompra, UrlMelhorCompra);


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
        private string RemoveExtraCharacters(string price)
        {
            // Remover "R" e "$"
            return price.Replace("R$", "").Trim();
        }
    }
}
