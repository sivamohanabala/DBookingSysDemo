using DBookingSysDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;


namespace DBookingSysDemo.Controllers
{
    public class BookingController : Controller
    {
       
        //HttpContext.Session.SetString("UserName", C.UserName);
        //            HttpContext.Session.SetString("UserId", C.UserId.ToString());
        string Baseurl = "https://localhost:44338/";
       public async Task<IActionResult> Index()
        {
            

            List < BookingDetail> BookingInfo = new List<BookingDetail>();
            List<Customer> CustomerInfo = new List<Customer>();
            List<Customer> ExecutiveInfo = new List<Customer>();


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
                    ExecutiveInfo = JsonConvert.DeserializeObject<List<Customer>>(CustomerResponse);

                }
                // return View(CustomerInfo);
                string city = HttpContext.Session.GetString("UserCity");

                 ExecutiveInfo = ExecutiveInfo.Where(s => s.City == city && s.UserType == "Delivery Executive").ToList();

                ViewBag.Locations = ExecutiveInfo;
              
                
            }


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
                return View(BookingInfo);

            }

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
          
            int UserId =Convert.ToInt32(HttpContext.Session.GetString("UID"));
            bool res = Convert.ToBoolean(HttpContext.Session.GetString("Verified"));
     if(res == true)
            { 
            List<Customer> CustomerInfo = new List<Customer>();
            List<Customer> ExecutiveInfo = new List<Customer>();


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
                    ExecutiveInfo = JsonConvert.DeserializeObject<List<Customer>>(CustomerResponse);
                    ///ExecutiveInfo = CustomerInfo;
                }
                // return View(CustomerInfo);
                string city = HttpContext.Session.GetString("UserCity");
                //Fruit f = (from x in ListOfFruits where x.Name == fruitName select x).First();
               ViewBag.userinfo = CustomerInfo.Where(s => s.UserId == UserId).ToList();
                //ViewBag.Addr = from Address in CustomerInfo where 

                 var ex = ExecutiveInfo.Where(s => s.City == city && s.UserType == "Delivery Executive").ToList();
                
                
                List<SelectListItem> listex = new List<SelectListItem>();

                foreach (var item in ex)
                {
                    listex.Add(new SelectListItem
                    {
                        Text = item.UserName,
                        Value= item.UserId.ToString()
                    });
                }
                ViewBag.ExecutiveName = listex;
              
            }


           // ViewBag.Locations = ExecutiveInfo;

            return View();
            }
            else
            {
                return RedirectToAction ("ErrorBook", "Accounts");
            }

        }

       

        [HttpPost]
        public async Task<IActionResult> Create(BookingDetail b)
        {
            //BookingDetail Bobj = new BookingDetail();
            ////  HttpClient obj = new HttpClient();

            //using (var httpClient = new HttpClient())
            //{
            //   httpClient.BaseAddress = new Uri(Baseurl);
            //    StringContent content = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");

            //    using (var response = await httpClient.PostAsync("api/BookingDetails/", content))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        Bobj = JsonConvert.DeserializeObject<BookingDetail>(apiResponse);
            //    }
            //}
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:44338/api/BookingDetails/", b);
           // HttpRequestMessage response=await htt
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            BookingDetail b = new BookingDetail();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/BookingDetails/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    b = JsonConvert.DeserializeObject<BookingDetail>(apiResponse);
                }
            }
            return View(b);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookingDetail b)
        {
            //BookingDetail b1 = new BookingDetail();
            //using (var httpClient = new HttpClient())
            //{
            //    int id = b.BookingId;
            //    StringContent content1 = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");
            //    using (var response = await httpClient.PutAsync("https://localhost:44338/api/BookingDetails/" + id, content1))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        ViewBag.Result = "Success";
            //        b1 = JsonConvert.DeserializeObject<BookingDetail>(apiResponse);
            //    }
            //}
            int bid = b.BookingId;
            var httpClient = new HttpClient();
           // HttpResponseMessage response = await httpClient.PutAsJsonAsync("https://localhost:44338/api/BookingDetails/", b);

            HttpResponseMessage res = await httpClient.PutAsJsonAsync("https://localhost:44338/api/BookingDetails/"+ bid, b);
            //PutAsJsonAsync
            res.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
            
        }
    }
}
