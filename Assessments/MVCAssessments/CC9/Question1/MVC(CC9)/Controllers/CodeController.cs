using MVC_CC9_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CC9_.Controllers;

namespace CodeController.Module
{
    public class CodeController : Controller
    {
        private NorthWindEntities db = new NorthWindEntities();
        public ActionResult CustomersInGermany()
        {
            var customers = db.Customers
                              .Where(c => c.Country == "Germany")
                              .ToList();

            return View(customers);
        }

        
        public ActionResult CustomerDetailsByOrder(int orderId = 10248)
        {
            var order = db.Orders
                          .Where(o => o.OrderID == orderId)
                          .Select(o => o.Customer)
                          .FirstOrDefault();

            return View(order);
        }
    }
}