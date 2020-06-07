using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LakewoodScoopScraped.Models;
using LakewoodScoopScraped.Data;

namespace LakewoodScoopScraped.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var scraper = new ScoopScraping();
            scraper.GetScoopArticles();
            return View(scraper.GetScoopArticles());
        }

    }
}
