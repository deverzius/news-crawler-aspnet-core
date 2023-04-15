using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HtmlAgilityPack;
using System.Net.Http;
using System.Linq;
using news_crawler.Models;

namespace news_crawler.Pages;

public class NewsCategoryModel : PageModel
{
	public List<News> newsList { get; set; }
	public NewsCategoryModel()
	{
	}

	public async void OnGet()
	{
		string type = HttpContext.Request.Query["type"].ToString();
		// Console.WriteLine(type);

		if (type.Equals("politics"))
			newsList = News.GetNews(@"https://vtv.vn/chinh-tri.htm");
		else if (type.Equals("society"))
			newsList = News.GetNews(@"https://vtv.vn/xa-hoi.htm");
		else if (type.Equals("law"))
			newsList = News.GetNews(@"https://vtv.vn/phap-luat.htm");
		else if (type.Equals("world"))
			newsList = News.GetNews(@"https://vtv.vn/the-gioi.htm");
		else if (type.Equals("economy"))
			newsList = News.GetNews(@"https://vtv.vn/kinh-te.htm");
		else if (type.Equals("technology"))
			newsList = News.GetNews(@"https://vtv.vn/cong-nghe.htm");
		else {
			newsList = new List<News>();
		}
	}

}
