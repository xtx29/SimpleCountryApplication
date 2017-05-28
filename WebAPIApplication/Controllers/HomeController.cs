using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using WebAPIApplication.Models;

namespace WebAPIApplication.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            try
            {
                //get rest data from the server
                using (HttpClient client = new HttpClient()) { 
                client.BaseAddress = new Uri("https://restcountries.eu");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("rest/v2/all").Result;
                if (response.IsSuccessStatusCode)
                {
                     //pass the data into viewbag to render view
                    ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Country>>().Result;
                }
                else
                {
                    ViewBag.result = "Error";
                }
                return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.result = ("Error: " + ex.Message);
                return View();
            }
        }
    }
}