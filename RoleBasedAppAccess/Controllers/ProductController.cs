using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoleBasedAppAccess.Models;
using RoleBasedAppAccess.CustomFilters;

namespace RoleBasedAppAccess.Controllers
{
    public class ProductController : Controller
    {
       

        // GET: Product
        public ActionResult Index()
        {
           
            return View();
        }

        [AuthLog(Roles = "Manager")]
        public ActionResult Create()
        {
           
            return View();
        }


      
       

        [AuthLog(Roles = "Sales Executive")]
        public ActionResult SaleProduct()
        {
            ViewBag.Message = "This View is designed for the Sales Executive to Sale Product.";
            return View();
        }
    }
}