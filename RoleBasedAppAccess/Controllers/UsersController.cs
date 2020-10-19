using RoleBasedAppAccess.CustomFilters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoleBasedAppAccess.Models;

namespace RoleBasedAppAccess.Controllers
{
    public class UsersController : Controller
    {
       [AuthLog(Roles = "User")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<Employee> EmployeeList = db.Employees.ToList<Employee>();
                return Json(new { data = EmployeeList },
                JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult StoreOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Employee());
            else
            {
                using (DBModel db = new DBModel())
                {


                    return View(db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>());

                }
            }
        }
    }

}