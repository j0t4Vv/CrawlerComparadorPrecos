using Crawler.Scraps;
using HtmlAgilityPack;
using System;
using Crawler.Data;
using Crawler.Models;
using Crawler.Utils;
public class MercadoLivreScraper
{
    public ProdutoScraper ObterPreco(string descricaoProduto, int idProduto)
    {
        // URL da pesquisa no Mercado Livre com base na descrição do produto
        string url = $"https://lista.mercadolivre.com.br/{descricaoProduto}";

        try
        {
            // Inicializa o HtmlWeb do HtmlAgilityPack
            HtmlWeb web = new HtmlWeb();

            // Carrega a página de pesquisa do Mercado Livre
            HtmlDocument document = web.Load(url);

            // Encontra o elemento que contém o preço do primeiro produto            
            HtmlNode firstProductPriceNode = document.DocumentNode.SelectSingleNode("//span[@class='andes-money-amount__fraction']");
            HtmlNode firstProductTitleNode = document.DocumentNode.SelectSingleNode("//h2[@class='ui-search-item__title']");
            HtmlNode firstProductUrlNode = document.DocumentNode.SelectSingleNode("//a[contains(@class, 'ui-search-link__title-card')]");

            // Verifica se o elemento foi encontrado
            if (firstProductPriceNode != null)
            {
                // Obtém o preço do primeiro produto
                ProdutoScraper produto = new ProdutoScraper();
                string firstProductPrice = firstProductPriceNode.InnerText.Trim();
                string firstProductName = firstProductTitleNode.InnerText.Trim();
                string firstProductUrl = firstProductUrlNode.GetAttributeValue("href", "");


                // Registra o log com o ID do produto
                RegistroLog.RegistrarLog("8538", "JoãoVictor", DateTime.Now, "WebScraping - Mercado Livre", "Sucesso", idProduto);

                produto.Price = firstProductPrice;
                produto.Name = firstProductName;
                produto.Url = firstProductUrl;

                // Retorna o preço
                return produto;
            }
            else
            {
                Console.WriteLine("Preço não encontrado.");

                // Registra o log com o ID do produto
                RegistroLog.RegistrarLog("8538", "JoãoVictor", DateTime.Now, "WebScraping - Mercado Livre", "Preço não encontrado", idProduto);

                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

            // Registra o log com o ID do produto
            RegistroLog.RegistrarLog("8538", "JoãoVictor", DateTime.Now, "Web Scraping - Mercado Livre", $"Erro: {ex.Message}", idProduto);

            return null;
        }
    }
}
