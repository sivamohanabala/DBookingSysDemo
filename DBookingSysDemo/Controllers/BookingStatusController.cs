using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBookingSysDemo.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace DBookingSysDemo.Controllers
{
    public class BookingStatusController : Controller
    {
        string Baseurl = "https://localhost:44338/";

        public async Task<IActionResult> Index()
        {
            string usertype = HttpContext.Session.GetString("UserType").ToString();

            if(usertype == "Customer")
            {
            List<BookingStatus> CustomerInfo = new List<BookingStatus>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/BookingStatus");

                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    CustomerInfo = JsonConvert.DeserializeObject<List<BookingStatus>>(CustomerResponse);

                }
                return View(CustomerInfo);

            }
            }
            else
            {
                return View("Error", "Accounts");
            }

            //return View();
        }

        [HttpGet]
        public IActionResult Track()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Track(string txtBookId)
        {
            int _id = Convert.ToInt32(txtBookId);
            return RedirectToAction("BookingHistory",new { id=_id});
        }

        [HttpGet]
        public async Task<IActionResult> Request()
        {
            string usertype = HttpContext.Session.GetString("UserType");
            string executive = HttpContext.Session.GetString("UID").ToString();
            //if (usertype != "Customer")
            //{

            List<BookingDetail> CustomerInfo = new List<BookingDetail>();

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("api/BookingDetails");

                    if (Res.IsSuccessStatusCode)
                    {
                        var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                        CustomerInfo = JsonConvert.DeserializeObject<List<BookingDetail>>(CustomerResponse);

                    }

                    CustomerInfo = CustomerInfo.Where(x => x.DExecutive == executive).ToList();
                    return View(CustomerInfo);

                }
            //}
            //else
            //{
            //    return RedirectToAction("Error", "Accounts");
            //}

            
        }

        


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            BookingStatus p = new BookingStatus();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/BookingDetails/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    p = JsonConvert.DeserializeObject<BookingStatus>(apiResponse);
                }
            }

            return View(p);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookingStatus c)
        {
            //BookingStatus p1 = new BookingStatus();
            //using (var httpClient = new HttpClient())
            //{
            //    int id = c.UserId;
            //    StringContent content1 = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");
            //    using (var response = await httpClient.PutAsync("https://localhost:44338/api/BookingStatus/" + id, content1))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        ViewBag.Result = "Success";
            //        p1 = JsonConvert.DeserializeObject<BookingStatus>(apiResponse);
            //    }
            //}
            
            var httpClient = new HttpClient();
            

            HttpResponseMessage res = await httpClient.PostAsJsonAsync("https://localhost:44338/api/BookingStatus/" , c);
            //PutAsJsonAsync
            res.EnsureSuccessStatusCode();

            return View("Index");
            
        }

        [HttpGet]
        public async Task<IActionResult> EditStatus(int id)
        {
            BookingStatus p = new BookingStatus();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/BookingStatus/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    p = JsonConvert.DeserializeObject<BookingStatus>(apiResponse);
                }
            }

            return View(p);

        }

        [HttpPost]
        public async Task<IActionResult> EditStatus(BookingStatus c)
        {
            BookingStatus p1 = new BookingStatus();
            //using (var httpClient = new HttpClient())
            //{
            //    int id = c.UserId;
            //    StringContent content1 = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");
            //    using (var response = await httpClient.PutAsync("https://localhost:44338/api/BookingStatus/" + id, content1))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        ViewBag.Result = "Success";
            //        p1 = JsonConvert.DeserializeObject<BookingStatus>(apiResponse);
            //    }
            //}
            //return RedirectToAction("Index");

            
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:44338/api/BookingStatus/", c);
          
            response.EnsureSuccessStatusCode();

            return View("Index");
        }

        public async Task<IActionResult> BookingHistory(int id)
        {
            List<BookingStatus> CustomerInfo = new List<BookingStatus>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/BookingStatus");

                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    CustomerInfo = JsonConvert.DeserializeObject<List<BookingStatus>>(CustomerResponse);

                }
                CustomerInfo = CustomerInfo.Where(x => x.BookingId == id).ToList();
                return View(CustomerInfo);

            }
        }
    }
}
