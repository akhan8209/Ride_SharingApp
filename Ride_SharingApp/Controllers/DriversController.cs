using PagedList;
using Ride_SharingApp.Authentications;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Ride_SharingApp.Controllers
{
   
    public class DriversController : Controller
    {
        private Ride_SharingDBEntities db = new Ride_SharingDBEntities();

        // GET: Drivers

        [Authorize(Roles = "AdminView")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var Contacts = from s in db.Drivers
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Contacts = Contacts.Where(s => s.DriverName.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    Contacts = Contacts.OrderByDescending(s => s.DriverName);
                    break;
              
                default:  // Name ascending 
                    Contacts = Contacts.OrderBy(s => s.DriverName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Contacts.ToPagedList(pageNumber, pageSize));
        }






       [Authorize(Roles = "UserView")]
       public ActionResult ReadOnly(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var Contacts = from s in db.Drivers
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Contacts = Contacts.Where(s => s.DriverName.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    Contacts = Contacts.OrderByDescending(s => s.DriverName);
                    break;

                default:  // Name ascending 
                    Contacts = Contacts.OrderBy(s => s.DriverName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Contacts.ToPagedList(pageNumber, pageSize));
        }

        //// GET: Drivers/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Driver driver = db.Drivers.Find(id);
        //    if (driver == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(driver);
        //}

        // GET: Drivers/Create
        [Authorize(Roles = "AdminView")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverId,DriverName,DriverCNIC,DriverEmail,DriverPhone,DriverAverageRating")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Drivers.Add(driver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(driver);
        }

        // GET: Drivers/Edit/5
        [Authorize(Roles = "AdminView")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverId,DriverName,DriverCNIC,DriverEmail,DriverPhone,DriverAverageRating")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(driver);
        }

        // GET: Drivers/Delete/5
        [Authorize(Roles = "AdminView")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Driver driver = db.Drivers.Find(id);
            db.Drivers.Remove(driver);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
