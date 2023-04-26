using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace Buscador.Models
{
    public class MercadoLivre
    {
        public static List<Produto> BuscarProdutos(string categoria, string pesquisa)
        {
            List<Produto> produtos = new List<Produto>();

            string url = "https://lista.mercadolivre.com.br/" + categoria;

            if (!string.IsNullOrEmpty(pesquisa))
            {
                url = url + "/" + pesquisa;
            }

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            foreach (HtmlNode produtoNode in doc.DocumentNode.SelectNodes("//li[contains(@class, 'ui-search-layout__item') or contains(@class, 'shops__layout-item')]").Take(3))
            {
                Produto produto = new Produto();

                HtmlNode linkNode = produtoNode.SelectSingleNode(".//a[@class='ui-search-link']");
                if (linkNode != null)
                {
                    produto.Link = linkNode.GetAttributeValue("href", "");
                }
                HtmlNode tituloNode = produtoNode.SelectSingleNode(".//h2[@class='ui-search-item__title shops__item-title']");
                if (tituloNode != null)
                {
                    produto.Titulo = tituloNode.InnerText.Trim();
                }

                HtmlNode precoNode = produtoNode.SelectSingleNode(".//span[@class='price-tag-fraction']");
                HtmlNode symbolNode = produtoNode.SelectSingleNode(".//span[@class='price-tag-symbol']");
                if (precoNode != null)
                {
                    if (symbolNode != null)
                    {
                        produto.Preco = symbolNode.InnerText + " ";
                    }

                    produto.Preco += precoNode.InnerText;
                }

                HtmlNode imagemNode = produtoNode.SelectSingleNode(".//img[@class='ui-search-result-image__element shops__image-element lazy-loadable']");
                if (imagemNode != null)
                {
                    produto.ImagemUrl = imagemNode.Attributes["data-src"].Value;
                }

                produtos.Add(produto);
            }

            return produtos;
        }
    }
}
