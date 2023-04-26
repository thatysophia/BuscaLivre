using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace Buscador.Models
{
    public class Buscape
    {

        public static List<Produto> BuscarProdutos(string categoria, string pesquisa)
        {
            List<Produto> produtos = new List<Produto>();

            string url = "https://www.buscape.com.br/search/" + categoria;

            if (!string.IsNullOrEmpty(pesquisa))
            {
                url = url + "/" + pesquisa;
            }

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            foreach (HtmlNode produtoNode in doc.DocumentNode.SelectNodes("//div[contains(@class, 'Paper_Paper__HIHv0 Paper_Paper__bordered__iuU_5 Card_Card__LsorJ Card_Card__clicable__5y__P SearchCard_ProductCard__1D3ve')]").Take(3))
            {
                Produto produto = new Produto();

                HtmlNode linkNode = produtoNode.SelectSingleNode("//a[@class='SearchCard_ProductCard_Inner__7JhKb']");
                if (linkNode != null)
                {
                    produto.Link = "https://www.buscape.com.br/"  + linkNode.GetAttributeValue("href", "");
                }
                HtmlNode tituloNode = produtoNode.SelectSingleNode(".//h2[contains(@class, 'Text_Text__h_AF6 Text_MobileLabelXs__ER_cD Text_DesktopLabelSAtLarge__baj_g SearchCard_ProductCard_Name__ZaO5o')]");
                if (tituloNode != null)
                {
                    produto.Titulo = tituloNode.InnerText.Trim();
                }

                HtmlNode precoNode = produtoNode.SelectSingleNode(".//p[contains(@class, 'Text_Text__h_AF6 Text_MobileHeadingS__Zxam2')]");
                if (precoNode != null)
                {
                    produto.Preco = precoNode.InnerText;
                }

                HtmlNode imagemNode = produtoNode.SelectSingleNode(".//img");
                if (imagemNode != null)
                {
                    produto.ImagemUrl = imagemNode.Attributes["src"].Value;
                }

                produtos.Add(produto);
            }

            return produtos;
        }
    }

}