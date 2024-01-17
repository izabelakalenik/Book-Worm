using Azure.Core;
using BookWorm.Data;
using BookWorm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

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
