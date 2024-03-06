using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Crawler.Compare;
using Crawler.Models;

namespace Crawler.Utils
{
    public class VerificadorProduto
    {
        // Lista para armazenar produtos já verificados
        static List<Produto> produtosVerificados = new List<Produto>();

        // Método para verificar novos produtos
        public static async void VerificarNovoProduto(object emailDestinoObj)
        {
            string emailDestino = emailDestinoObj as string; // Converte o objeto para string

            string username = "11164448";
            string senha = "60-dayfreetrial";
            string url = "http://regymatrix-001-site1.ktempurl.com/api/v1/produto/getall";

            try
            {
                // Criar um objeto HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Adicionar as credenciais de autenticação básica
                    var byteArray = Encoding.ASCII.GetBytes($"{username}:{senha}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    // Fazer a requisição GET à API
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Verificar se a requisição foi bem-sucedida (código de status 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Ler o conteúdo da resposta como uma string
                        string responseData = await response.Content.ReadAsStringAsync();

                        // Processar os dados da resposta
                        List<Produto> novosProdutos = ProcessarProduto.ObterNovosProdutos(responseData);
                        foreach (Produto produto in novosProdutos)
                        {
                            if (!produtosVerificados.Exists(p => p.Id == produto.Id))
                            {
                                // Se é um novo produto, faça algo com ele
                                Console.WriteLine($"Novo produto encontrado: ID {produto.Id}, Nome: {produto.Nome}");
                                // Adicionar o produto à lista de produtos verificados
                                produtosVerificados.Add(produto);

                                // Registra um log no banco de dados apenas se o produto for novo
                                if (!VerificadorRegistro.ProdutoJaRegistrado(produto.Id))
                                {
                                    RegistroLog.RegistrarLog("8538", "JoãoVictor", DateTime.Now, "ConsultaAPI - Verificar Produto", "Sucesso", produto.Id);

                                    MercadoLivreScraper mercadoLivreScraper = new MercadoLivreScraper();
                                    var precoMercadoLivre = mercadoLivreScraper.ObterPreco(produto.Nome, produto.Id);

                                    MagazineLuizaScraper magazineLuizaScraper = new MagazineLuizaScraper();
                                    var precoMagazineLuiza = magazineLuizaScraper.ObterPreco(produto.Nome, produto.Id);

                                    Benchmark benchmark = new Benchmark();

                                    // Comparar preços passando o endereço de e-mail como argumento
                                    benchmark.CompararPrecos(produto.Nome, precoMagazineLuiza, precoMercadoLivre, emailDestino);

                                    // Registrar log do benchmark
                                    RegistroLog.RegistrarLog("8538", "JoãoVictor", DateTime.Now, "Benchmark - Comparar Preços", "Sucesso", produto.Id);

                                    //// Enviar e-mail com os resultados da comparação
                                    //Email.EnviarEmail(produto.Nome, precoMercadoLivre, produto.Nome, precoMagazineLuiza);

                                    // Registrar o envio do e-mail no log
                                    RegistroLog.RegistrarLog("8538", "JoãoVictor", DateTime.Now, "EnvioEmail", "Sucesso", produto.Id);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Imprimir mensagem de erro caso a requisição falhe
                        Console.WriteLine($"Erro: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Imprimir mensagem de erro caso ocorra uma exceção
                Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            }
        }
    }
}
