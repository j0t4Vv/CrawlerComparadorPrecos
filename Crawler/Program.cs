using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Crawler.Utils;
using Crawler.Send;

class Program
{
    static void Main(string[] args)
    {

        // Definir o intervalo de tempo para 5 minutos (300.000 milissegundos)
        int intervalo = 6000;

        // Criar um temporizador que dispara a cada 5 minutos
        Timer timer = new Timer(VerificadorProduto.VerificarNovoProduto, null, 0, intervalo);

        // Manter a aplicação rodando até que a tecla "0" seja pressionada
        Console.WriteLine("Pressione a tecla '0' para sair...");
        while (Console.ReadKey().Key != ConsoleKey.D0) { }

    }
}
