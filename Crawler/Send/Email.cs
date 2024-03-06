using System;
using System.Net;
using System.Net.Mail;

namespace Crawler.Send
{
    public class Email
    {
        public static string SolicitarEmailDestino()
        {
            Console.WriteLine("Por favor, insira o endereço de email para receber o resultado: ");
            return Console.ReadLine();
        }

        public static void EnviarEmail(string emailDestino, string nomeProdutoMercadoLivre, string precoProdutoMercadoLivre, string nomeProdutoMagazineLuiza, string precoProdutoMagazineLuiza, string MelhorCompra, string UrlMelhorCompra)
        {
            // Verificar se o endereço de e-mail de destino é nulo ou vazio
            if (string.IsNullOrEmpty(emailDestino))
            {
                Console.WriteLine("O endereço de e-mail de destino é nulo ou vazio.");
                return;
            }

            // Configurações do servidor SMTP do Gmail
            string smtpServer = "smtp-mail.outlook.com"; // Servidor SMTP do Gmail
            int porta = 587; // Porta SMTP do Gmail para TLS/STARTTLS
            string remetente = "joaov.so@outlook.com"; // Seu endereço de e-mail do Gmail
            string senha = "testecrawler123"; // Sua senha do Gmail

            // Configurar cliente SMTP
            using (SmtpClient client = new SmtpClient(smtpServer, porta))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(remetente, senha);
                client.EnableSsl = true; // Habilitar SSL/TLS

                // Construir mensagem de e-mail
                MailMessage mensagem = new MailMessage(remetente, emailDestino)
                {
                    Subject = "Resultado do Crawler de comparação de preços",
                    Body = $"Mercado Livre\n Produto: {nomeProdutoMercadoLivre}\n Preço: {precoProdutoMercadoLivre}\n" +
                           $"Magazine Luiza\n Produto: {nomeProdutoMagazineLuiza}\n Preço: {precoProdutoMagazineLuiza}\n" +
                           $"Melhor Compra\n {MelhorCompra} - {UrlMelhorCompra}"
                };

                // Enviar e-mail
                client.Send(mensagem);

                Console.WriteLine("E-mail enviado com sucesso.");
            }
        }
    }
}
