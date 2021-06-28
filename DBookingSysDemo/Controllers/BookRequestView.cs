using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBookingSysDemo.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DBookingSysDemo.Controllers
{
    public class BookRequestView : Controller
    {
       // string UName = HttpContext.Session.GetString("UserName");
        string Baseurl = "https://localhost:44338/";

        List<Customer> CustomerInfo = new List<Customer>();

        List<BookingDetail> BookingInfo = new List<BookingDetail>();
        public async Task<IActionResult> Index()
        {
           
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/BookingDetails");

                if (Res.IsSuccessStatusCode)
                {
                    var BookingResponse = Res.Content.ReadAsStringAsync().Result;
                    BookingInfo = JsonConvert.DeserializeObject<List<BookingDetail>>(BookingResponse);

                }
                //return View(BookingInfo);

            }
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Customers");

                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    CustomerInfo = JsonConvert.DeserializeObject<List<Customer>>(CustomerResponse);

                }
               // return View(CustomerInfo);

            }
         
            List<CustomerBookDetView> model = new List<CustomerBookDetView>();
            var innerJoinQuery = from c in CustomerInfo
                                    join b in BookingInfo on c.UserId equals b.UserId
                                    select new { c.UserName, c.Address, c.City,c.Phone, b.Package, b.PickUp, b.Status, b.Price };
            foreach (var item in innerJoinQuery) //retrieve each item and assign to model
            {
                model.Add(new CustomerBookDetView()
                {
                    UserName = item.UserName,
                    Address = item.Address,
                    City = item.City,
                    Phone = item.Phone,
                    Package=item.Package,
                    PickUp=item.PickUp,
                    Status=item.Status,
                    Price=item.Price
                });
            }
            return View(model);

            //return View(ViewData["JoinTable"]);
        }
    }
}
