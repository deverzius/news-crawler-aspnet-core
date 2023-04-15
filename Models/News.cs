using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HtmlAgilityPack;
using System.Net.Http;
using System.Linq;
using System;
using System.Collections.Generic;

namespace news_crawler.Models;

public class News {
	public String? title { get; set; }
	public String? detail { get; set; }
	public String? imageUrl;
	public String? urlToNews { get; set; }
	public LinkedList<String>? content { get; set; }

	private static List<News> newsList = new List<News>();

	public static List<News> GetNews(String url) {
        HtmlWeb web = new HtmlWeb();
        var htmlDoc = web.Load(url);

		var divs = htmlDoc.DocumentNode.SelectNodes("//li[@class='tlitem ']");
		newsList.Clear();

		foreach (var div in divs) {
			var news = new News
			{
				title = div.SelectSingleNode(".//h4").InnerText,
				detail = div.Descendants("p").Where(d => d.HasClass("sapo")).ElementAt(0).InnerText,
				imageUrl = div.Descendants("img").FirstOrDefault().Attributes["src"].Value,
				urlToNews = "https://vtv.vn" + div.SelectSingleNode(".//h4/a").Attributes["href"].Value,
			};

			// var singleNewsPage = web.Load(news.urlToNews);
			//var content = singleNewsPage.DocumentNode.SelectSingleNode("//div[@id='entry-body]").InnerText;
			//Console.WriteLine(content);
			newsList.Add(news);
		}

		return newsList;
	} 

	public static News GetSingleNews(string title) {
		foreach (var news in newsList) {
			if (news.title == title) {
				HtmlWeb web = new HtmlWeb();

				//Console.WriteLine("https://vtv.vn" + news.urlToNews);
				var htmlDoc = web.Load(news.urlToNews);

				var paras = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='entry-body']").Descendants("p").ToList();

				var content = new LinkedList<String>();
				foreach (var para in paras) {
					content.AddLast(para.InnerText);
				}
				content.RemoveLast();
				//Console.WriteLine(content);
				news.content = content;

				return news;
			}
		}
		return null;
	}
}