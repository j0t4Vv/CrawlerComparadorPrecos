using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Models;
using Newtonsoft.Json;

namespace Crawler.Utils;

public static class ProcessarProduto
{
    // Método para processar os dados da resposta e obter produtos
    public static List<Produto> ObterNovosProdutos(string responseData)
    {
        // Desserializar os dados da resposta para uma lista de produtos
        List<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(responseData);
        return produtos;
    }
}
