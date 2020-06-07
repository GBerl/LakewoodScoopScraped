using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace LakewoodScoopScraped.Data
{
    public class ScoopScraping
    {

        public List<Article> GetScoopArticles()
        {
            var scoopArticles = new List<Article>();
            var html = GetHtml();
            var parser = new HtmlParser();
            var htmlDocument = parser.ParseDocument(html);
            var divs = htmlDocument.QuerySelectorAll(".post");
            foreach (var div in divs)
            {
                var article = GetArticle(div);
                if (article != null)
                {
                    scoopArticles.Add(article);
                }
            }

            return scoopArticles;
        }

        private Article GetArticle(IElement div)
        {
            var title = div.QuerySelector("h2");
            if (title == null)
            {
                return null;
            }
            var article = new Article
            {
                Title = title.TextContent,
                TitleUrl = title.QuerySelector("a").Attributes["href"].Value
            };
            var content = div.QuerySelector("p");
            if (content != null)
            {
                article.BlurbOfText = content.Text();

                var image = content.QuerySelector("img");
                if (image != null)
                {
                    article.ImageUrl = image.Attributes["src"].Value;
                }
            }
            var comment = div.QuerySelector(".backtotop");

            article.Comments = comment.TextContent;

            return article;
        }
        static string GetHtml()
        {
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);
            var html = client.GetStringAsync($"https://www.thelakewoodscoop.com/").Result;
            return html;
        }
    }
}
