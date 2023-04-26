using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Buscador
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Configuração da rota para a ação Buscar no controlador ProdutosController
            routes.MapRoute(
                name: "BuscarProdutos",
                url: "Produtos/Buscar",
                defaults: new { controller = "Produtos", action = "Buscar" }
            );

            // Configuração da rota padrão para a página inicial
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}