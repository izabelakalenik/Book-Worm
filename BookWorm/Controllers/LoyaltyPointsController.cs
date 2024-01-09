using Azure.Core;
using BookWorm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookWorm.Controllers
{
    public class LoyaltyPointsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.LoyaltyPoints = 0;

            return View();
        }

    }
}
