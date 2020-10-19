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

    public class AdminController : Controller
    {
       [AuthLog(Roles = "Administrator")]
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

        [HttpPost]
        public ActionResult StoreOrEdit(Employee employeeob)
        {
            using (DBModel db = new DBModel())
            {
                if (employeeob.EmployeeID == 0)
                {
                    db.Employees.Add(employeeob);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    db.Entry(employeeob).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
                }
            }

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                Employee emp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
        }
    }
}