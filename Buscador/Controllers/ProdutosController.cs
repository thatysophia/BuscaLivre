using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buscador.Models;

namespace Buscador.Controllers
{
    public class ProdutosController : Controller
    {
        public ActionResult Index(string categoria, string pesquisa, string loja = "todas")
        {
            List<Produto> produtosMercadoLivre = new List<Produto>();
            List<Produto> produtosBuscape = new List<Produto>();

            if (loja == "mercado-livre" || loja == "todas")
            {
                produtosMercadoLivre = MercadoLivre.BuscarProdutos(categoria, pesquisa);
            }

            if (loja == "buscape" || loja == "todas")
            {
                produtosBuscape = Buscape.BuscarProdutos(categoria, pesquisa);
            }

            List<Produto> produtosCombinados = produtosMercadoLivre.Concat(produtosBuscape).ToList();

            ViewBag.Categoria = categoria;
            ViewBag.ProdutosCombinados = produtosCombinados;

            return View("~/Views/Home/Index.cshtml");
        }
    }
}