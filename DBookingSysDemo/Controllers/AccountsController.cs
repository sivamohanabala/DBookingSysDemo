using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBookingSysDemo.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using System.Web;
using System.Web.Providers.Entities;

namespace DBookingSysDemo.Controllers
{

    public class AccountsController : Controller
    {
        string Baseurl = "https://localhost:44338/";
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register()
        {
            return View("Create", "Customer");
        }
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]

        public async Task<IActionResult> Login(Customer C)
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


                //   if (IsValidUser)
                //   {
                //       FormsAuthentication.SetAuthCookie(model.UserName, false);
                //       return RedirectToAction("Index", "Employees");
                //   }
                //ModelState.AddModelError("", "invalid Username or Password");
                //return View();
                //return View(CustomerInfo);

                bool IsValidUser = CustomerInfo.Any(user => user.UserName.ToLower() == C.UserName.ToLower()
                                  && user.Password == C.Password && user.UserType == C.UserType);
                Customer obj = (from i in CustomerInfo
                                where i.UserName == C.UserName && i.Password == C.Password &&
                                i.UserType == C.UserType
                                select i).FirstOrDefault();
                if(obj != null && obj.IsVerified != true)
                {
                    return RedirectToAction("ErrorBook");
                }

                if (IsValidUser)
                {
                    //HttpContent.cuSession.SetString("UserName", C.UserName);
                    //FormsAuthentication.SetAuthCookie(C.UserName, false);
                    var curres = CustomerInfo.Where(s => s.UserName == C.UserName && s.UserType == C.UserType);

                    string UserId = obj.UserId.ToString();
                    HttpContext.Session.SetString("UserName", C.UserName);
                    HttpContext.Session.SetString("UID", obj.UserId.ToString());
                    HttpContext.Session.SetString("UserType", obj.UserType);
                    HttpContext.Session.SetString("UserCity",obj.City);
                    HttpContext.Session.SetString("Verified", obj.IsVerified.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }

            }

        }

            public IActionResult LogOut()
            {
                // FormsAuthentication.SignOut();
                return RedirectToAction("Login");
            }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult ErrorBook()
        {
            return View();
        }
      
        
    }


}
