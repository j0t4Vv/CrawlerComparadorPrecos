using Crawler.Scraps;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Crawler.Data;
using Crawler.Models;
using Crawler.Utils;

public class MagazineLuizaScraper
{
    public ProdutoScraper ObterPreco(string descricaoProduto, int idProduto)
    {
        try
        {
            // Inicializa o ChromeDriver
            using (IWebDriver driver = new ChromeDriver())
            {
                // Abre a página
                driver.Navigate().GoToUrl($"https://www.magazineluiza.com.br/busca/{descricaoProduto}");

                // Aguarda um tempo fixo para permitir que a página seja carregada (você pode ajustar conforme necessário)
                System.Threading.Thread.Sleep(5000);

                // Encontra o elemento que possui o atributo data-testid

                IWebElement priceElement = driver.FindElement(By.CssSelector("[data-testid='price-value']"));
                IWebElement titleElement = driver.FindElement(By.CssSelector("[data-testid='product-title']"));
                IWebElement urlElement = driver.FindElement(By.CssSelector("[data-testid='product-card-container']"));

                // Verifica se o elemento foi encontrado
                if (priceElement != null)
                {
                    // Obtém o preço do primeiro produto
                    ProdutoScraper produto = new ProdutoScraper();
                    string firstProductPrice = priceElement.Text.Trim();
                    string firstProductName = titleElement.Text.Trim();
                    string firstProductUrl = urlElement.GetAttribute("href");

                    // Registra o log com o ID do produto
                    RegistroLog.RegistrarLog("8538", "JoãoVictor", DateTime.Now, "WebScraping - Magazine Luiza", "Sucesso", idProduto);

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
                    RegistroLog.RegistrarLog("8538", "JoãoVictor", DateTime.Now, "WebScraping - Magazine Luiza", "Preço não encontrado", idProduto);

                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

            // Registra o log com o ID do produto
            RegistroLog.RegistrarLog("0000088538", "JoãoVictor", DateTime.Now, "Web Scraping - Magazine Luiza", $"Erro: {ex.Message}", idProduto);

            return null;
        }
    }
}
