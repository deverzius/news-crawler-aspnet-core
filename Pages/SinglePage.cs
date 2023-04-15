using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HtmlAgilityPack;
using System.Net.Http;
using System.Linq;
using System.Web;
using news_crawler.Models;

namespace news_crawler.Pages;

public class SingleNewsPage : PageModel
{
	public SingleNewsPage()
	{
	}
	public News news;

	public async void OnGet()
	{
		var title = HttpContext.Request.Query["news_title"].ToString();
		news = News.GetSingleNews(title);
		
		if (news == null) {
			return;
		}
	}
}
