using foodDeliveryEPA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace foodDeliveryEPA.Controllers
{
    public class HomeController : Controller
    { 
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetEmployees()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var employees = dc.Employees.OrderBy(a => a.FirstName).ToList();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Save(int id)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Employees.Where(a => a.EmployeeId == id).FirstOrDefault();
                return View(v);
            }
        }
		[HttpPost]
		public ActionResult Save(Employee emp)

		{
			bool status = false;
			if (ModelState.IsValid)

			{
				using (MyDatabaseEntities dc = new MyDatabaseEntities())
				{
					if (emp.EmployeeId > 0)
					{
						//edit
						var v = dc.Employees.Where(a => a.EmployeeId == emp.EmployeeId).FirstOrDefault();
						if (v != null)
						{
							v.FirstName = emp.FirstName;
							v.LastName = emp.LastName;
							v.EmailID = emp.EmailID;
							v.City = emp.City;
							v.Vehicle = emp.Vehicle;
						 }
					}
					else
					{
						//save
						dc.Employees.Add(emp);
					}
					dc.SaveChanges();
					status = true;
					{ Response.Redirect("~/Home/Index"); }

				}
			}
			return new JsonResult { Data = new { status = status } };
		}


		[HttpGet]
		public ActionResult Delete(int id)
		{
			using (MyDatabaseEntities dc = new MyDatabaseEntities())
			{
				var v = dc.Employees.Where(a => a.EmployeeId == id).FirstOrDefault();
				if (v != null)
				{
					return View(v);
				}
				else
				{
					return HttpNotFound();
				}
			}
		}

		[HttpPost]
		[ActionName("Delete")]
				public ActionResult DeleteEmployee(int id)
		{
			bool status = false;
			using (MyDatabaseEntities dc = new MyDatabaseEntities())
			{

				var v = dc.Employees.Where(a => a.EmployeeId == id).FirstOrDefault();
				if (v != null)
				{
					dc.Employees.Remove(v);
					dc.SaveChanges();
					status = true;
					{ Response.Redirect("~/Home/Index"); }
				}

			}
			return new JsonResult { Data = new { status = status } };
		}
	}
}