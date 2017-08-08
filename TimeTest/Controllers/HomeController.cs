using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveData(List<CCLG> costCode)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (TimeTestDBEntities dc = new TimeTestDBEntities())
                {
                    foreach (var i in costCode)
                    {
                        dc.CCLGs.Add(i);
                        dc.CCLGs.
                    }
                    try
                    {
                        dc.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                    status = true;
                }
            }

            return new JsonResult { Data = new { status = status } };
        }
    }
}