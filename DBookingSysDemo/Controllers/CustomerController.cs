using DBookingSysDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DBookingSysDemo.Controllers
{
    public class CustomerController : Controller
    {
        string Baseurl = "https://localhost:44338/";
        public async Task<IActionResult> IndexAsync()
        {
            string usertyp = HttpContext.Session.GetString("UserType");
            if( usertyp == "Admin")
            {
            List<Customer> CustomerInfo = new List<Customer>();

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
                return View(CustomerInfo);

            }
            }
            else
            {
                return RedirectToAction("Error", "Accounts");
            }
            //return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Customer c)
        {
            ///api/Customers -- 44366
            Customer Cobj = new Customer();
            //  HttpClient obj = new HttpClient();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/Customers", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Cobj = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return RedirectToAction("Login","Accounts");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Customer p = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Customers/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    p = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            
            return View(p);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer c)
        {
            Customer p1 = new Customer();
            using (var httpClient = new HttpClient())
            {
                int id = c.UserId;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44338/api/Customers/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    p1 = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> GetAllPro()
        //{
        //    List<Product> ProductInfo = new List<Product>();

        //    using (var client = new HttpClient())
        //    {

        //        client.BaseAddress = new Uri(Baseurl);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage Res = await client.GetAsync("api/Products");

        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var ProductResponse = Res.Content.ReadAsStringAsync().Result;
        //            ProductInfo = JsonConvert.DeserializeObject<List<Product>>(ProductResponse);

        //        }
        //        return View(ProductInfo);

        //    }
        //}
    }
}
