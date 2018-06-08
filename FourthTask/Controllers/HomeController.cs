using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FourthTask.Models;

namespace FourthTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CustomersList(string searchString)
        {
            IEnumerable<Customer> CustInfo = new List<Customer>();
            string Baseurl = "http://localhost:4004";
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("customers");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CustResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    CustInfo = JsonConvert.DeserializeObject<List<Customer>>(CustResponse);

                }
                if (!String.IsNullOrEmpty(searchString))
                {
                    CustInfo = CustInfo.Where(s => s.ContactName.Contains(searchString));
                }
                //returning the employee list to view  
                return View(CustInfo);
            }

        }

        public async Task<ActionResult> CustomerOrders(string id)
        {
            List<Order> OrdersInfo = new List<Order>();
            Customer CustInfo = new Customer();
            string Baseurl = "http://localhost:4004";
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("customer/" + id +"/orders");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var OrdersResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    OrdersInfo = JsonConvert.DeserializeObject<List<Order>>(OrdersResponse);

                }

                HttpResponseMessage ResSecond = await client.GetAsync("customer/" + id );

                if (ResSecond.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CustResponse = ResSecond.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    CustInfo = JsonConvert.DeserializeObject<Customer>(CustResponse);

                }
                CustomerOrdersModel viewModel = new CustomerOrdersModel();
                viewModel.CurrCustomer = CustInfo;
                viewModel.CustomerOrders = OrdersInfo;
                //returning the employee list to view  
                return View(viewModel);
            }
        }

    }
}
