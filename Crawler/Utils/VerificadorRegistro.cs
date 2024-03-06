using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Crawler.Data;
using Crawler.Models;

namespace Crawler.Utils;

public static class VerificadorRegistro
{
    // Método para verificar se o produto já foi registrado no banco de dados
    public static bool ProdutoJaRegistrado(int idProduto)
    {
        using (var context = new LogContext())
        {
            return context.LOGROBO.Any(log => log.IdProdutoAPI == idProduto);
        }
    }
}
