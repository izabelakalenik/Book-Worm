using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookWorm.Data;
using BookWorm.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace BookWorm.Controllers
{
    [Authorize]
    [Authorize(Policy = "RestrictAdmin")]
    public class OrdersController : Controller
    {
        private readonly MyDbContext _context;

        public OrdersController(MyDbContext context)
        {
            _context = context;
        }


        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            string cartString = Request.Cookies["cart"];
            Dictionary<int, int> cart;
            if (cartString != null)
            {
                cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cartString);
            }
            else
            {
                cart = new Dictionary<int, int>();
            }


            var articles = _context.Article.ToList();
            var articlesQuantities = new List<(Article, int)>();
            ViewBag.Total = 0;
            foreach (var item in cart)
            {
                var article = articles.Where(a => a.ArticleId == item.Key).ToList();

                if (article != null && article.Count != 0)
                {
                    ViewBag.Total += article[0].Price * item.Value;
                    articlesQuantities.Add((article[0], item.Value));
                }
                else
                {
                    cart.Remove(item.Key);
                }
            }
            var order = new Order()
            {
                OrderedItems = articlesQuantities
            };

            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Address,Payment, OrderedItems")] Order order)
        {


            if (ModelState.IsValid)
            {
                string cartString = Request.Cookies["cart"];
                Dictionary<int, int> cart;
                cart = new Dictionary<int, int>();
                cartString = JsonConvert.SerializeObject(cart);
                SetCookie("cart", cartString, 7);
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = order.Id });
            }
            return View(order);
        }


        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        public void SetCookie(string key, string value, int? numberOfDays = null)
        {
            CookieOptions option = new CookieOptions();
            if (numberOfDays.HasValue)
                option.Expires = DateTime.Now.AddDays(numberOfDays.Value);
            Response.Cookies.Append(key, value, option);
        }
    }
}
