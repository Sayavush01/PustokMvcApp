using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_WEB_APP.Models;
using MVC_WEB_APP.ViewModels;
using Newtonsoft.Json;
using PustokMvcApp.Data;

namespace MVC_WEB_APP.Controllers
{
    public class BasketController(PustokMvcAppDbContext appContex) : Controller
    {
        public IActionResult AddBasket(Guid id)
        {
            var book = appContex.Books.FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            // If user is logged in, save basket to database
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = appContex.Users
                    .FirstOrDefault(x => x.UserName == User.Identity.Name);

                if (user == null)
                {
                    return NotFound();
                }

                var existUserBasketItem = appContex.BasketItems
                    .FirstOrDefault(x => x.BookId == book.Id && x.AppUserId == user.Id);

                if (existUserBasketItem == null)
                {
                    appContex.BasketItems.Add(new BasketItem
                    {
                        Id = Guid.NewGuid(),
                        BookId = book.Id,
                        AppUserId = user.Id,
                        Count = 1
                    });
                }
                else
                {
                    existUserBasketItem.Count++;
                }

                appContex.SaveChanges();

                var userBasketItems = GetBasketItems();

                return PartialView("_BasketPartial", userBasketItems);
            }

            // If user is not logged in, save basket to cookie
            List<BasketItemVm> basketItems;

            var basket = Request.Cookies["basket"];

            if (string.IsNullOrEmpty(basket))
            {
                basketItems = new List<BasketItemVm>();
            }
            else
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItemVm>>(basket)
                              ?? new List<BasketItemVm>();
            }

            var existBasketItem = basketItems.FirstOrDefault(x => x.BookId == book.Id);

            if (existBasketItem == null)
            {
                basketItems.Add(new BasketItemVm
                {
                    BookId = book.Id,
                    Name = book.Name,
                    Price = book.DiscountPercentage > 0
                        ? book.Price * (100 - book.DiscountPercentage) / 100
                        : book.Price,
                    Count = 1,
                    MainImageUrl = book.MainImageUrl
                });
            }
            else
            {
                existBasketItem.Count++;
            }

            Response.Cookies.Append(
                "basket",
                JsonConvert.SerializeObject(basketItems),
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(7)
                });

            return PartialView("_BasketPartial", basketItems);
        }


        private List<BasketItemVm> GetBasketItems()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = appContex.Users
                    .FirstOrDefault(x => x.UserName == User.Identity.Name);

                if (user == null)
                {
                    return new List<BasketItemVm>();
                }

                return appContex.BasketItems
                    .Include(x => x.Book)
                    .Where(x => x.AppUserId == user.Id)
                    .Select(x => new BasketItemVm
                    {
                        BookId = x.BookId,
                        Name = x.Book.Name,
                        Price = x.Book.DiscountPercentage > 0
                            ? x.Book.Price * (100 - x.Book.DiscountPercentage) / 100
                            : x.Book.Price,
                        Count = x.Count,
                        MainImageUrl = x.Book.MainImageUrl
                    })
                    .ToList();
            }

            var basket = Request.Cookies["basket"];

            if (string.IsNullOrEmpty(basket))
            {
                return new List<BasketItemVm>();
            }

            return JsonConvert.DeserializeObject<List<BasketItemVm>>(basket)
                   ?? new List<BasketItemVm>();
        }

        [Authorize(Roles = "User")]
        public IActionResult CheckOut()
        {
            CheckOutVm checkOutVm = new CheckOutVm();
            var user = appContex.Users
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Book)
                .FirstOrDefault(x => x.UserName == User.Identity.Name);
            checkOutVm.CheckOutItemVms = user.BasketItems.Select(x => new CheckOutItemVm
            {
               
                Name = x.Book.Name,
                Price = x.Book.DiscountPercentage > 0
                    ? x.Book.Price * (100 - x.Book.DiscountPercentage) / 100
                    : x.Book.Price,
                Count = x.Count,
              
            }).ToList();
            var basket = Request.Cookies["basket"];



            return View(checkOutVm);

        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult CheckOut(CheckOutVm checkOutVm)
        {
            var user = appContex.Users
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Book)
                .FirstOrDefault(x => x.UserName == User.Identity.Name);
         
            if (!ModelState.IsValid)
            {
                
                checkOutVm.CheckOutItemVms = user.BasketItems.Select(x => new CheckOutItemVm
                {
                    Name = x.Book.Name,
                    Price = x.Book.DiscountPercentage > 0
                        ? x.Book.Price * (100 - x.Book.DiscountPercentage) / 100
                        : x.Book.Price,
                    Count = x.Count,
                }).ToList();
                return View(checkOutVm);
            }
            var order = new Order
            {
                Id = Guid.NewGuid(),
                AppUserId = user.Id,
                ZipCode = checkOutVm.OrderVm.ZipCode,
                State = checkOutVm.OrderVm.State,
                TownCity = checkOutVm.OrderVm.TownCity,
                Address = checkOutVm.OrderVm.Address,
                CreatedDate = DateTime.Now,
                TotalPrice = user.BasketItems.Sum(x => x.Count * (x.Book.DiscountPercentage > 0
                    ? x.Book.Price * (100 - x.Book.DiscountPercentage) / 100
                    : x.Book.Price)),
                OrderItems = user.BasketItems.Select(x => new OrderItem
                {
            
                    BookId = x.BookId,
                    Count = x.Count,
                    Price = x.Book.DiscountPercentage > 0
                        ? x.Book.Price * (100 - x.Book.DiscountPercentage) / 100
                        : x.Book.Price
                }).ToList()
            };
            appContex.Orders.Add(order);
            appContex.BasketItems.RemoveRange(user.BasketItems);
            appContex.SaveChanges();
            Response.Cookies.Delete("basket", new() { Expires = DateTimeOffset.Now.AddDays(-1) }) ;
            return RedirectToAction("Index", "Home");



        }
        public IActionResult SetCookie()
        {
            Response.Cookies.Append("Pustok", "Hello Pustok");
            return Content("Cookie has been set!");
        }
        public IActionResult GetCookie()
        {
            var cookieValue = Request.Cookies["Pustok"];
            if (cookieValue != null)
            {
                return Content($"Cookie Value: {cookieValue}");
            }
            else
            {
                return Content("Cookie not found.");
            }
        }
        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("PustokSession", "Hello Pustok Session");
            return Content("Session has been set!");
        }

    }
}
