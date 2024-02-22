using Hello_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Hello_MVC.Controllers
{
    public class HomeController : Controller
    {
        ObjectCache cache = MemoryCache.Default;

        List<Customer> customers;

       
        public HomeController()
        {
            customers = cache["customers"] as List<Customer>;

            if (customers == null)
            {
                customers = new List<Customer>();
            }


        }

        public void SaveCache()
        {
            cache["customers"] = customers;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Viewcustomer(string name ,string telephone)
        {
            Customer customer = new Customer();
            customer.Id = Guid.NewGuid().ToString();
            customer.Name = name;
            customer.Telephone = telephone;
            return View(customer);

        }
        public ActionResult Addcustomer()
        {
            Customer customer = new Customer();
            customer.Id = Guid.NewGuid().ToString();
            
            return View(customer);

        }
        [HttpPost]
        public ActionResult Addcustomer(Customer customer)
        {

            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            customer.Id = Guid.NewGuid().ToString();
            customers.Add(customer);
            SaveCache();


            return RedirectToAction("CustomerList");

        }

        public ActionResult Editcustomer(string id) {

            Customer customer = customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();

            }
            else
            {
                return View(customer);
            }
        }
        [HttpPost]
        public ActionResult Editcustomer(string id,Customer customer)
        {

           Customer editcustomer = customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();

            }
            else
            {
                editcustomer.Name = customer.Name;
                editcustomer.Telephone=customer.Telephone;
                SaveCache();

                return RedirectToAction("CustomerList");
                 
            }
        }
        public ActionResult CustomerDetails(string id)
        {
            Customer customer = customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();

            }
            else
            {
                return View(customer);
            }
        }
        public ActionResult CustomerList()
        {
           
            return View(customers);
        }
        public ActionResult DeleteCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();

            }
            else
            {
                customers.Remove(customer);
                SaveCache();
                return RedirectToAction("CustomerList");
            }
        }
    }
}