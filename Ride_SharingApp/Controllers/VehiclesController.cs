using PagedList;
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
    public class VehiclesController : Controller
    {
        private Ride_SharingDBEntities db = new Ride_SharingDBEntities();

        // GET: Vehicles
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

            var Contacts = from s in db.Vehicles
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Contacts = Contacts.Where(s => s.VehicleOwnerName.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    Contacts = Contacts.OrderByDescending(s => s.VehicleOwnerName);
                    break;

                default:  // Name ascending 
                    Contacts = Contacts.OrderBy(s => s.VehicleOwnerName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Contacts.ToPagedList(pageNumber, pageSize));
        }


        // User 

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

            var Contacts = from s in db.Vehicles
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Contacts = Contacts.Where(s => s.VehicleOwnerName.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    Contacts = Contacts.OrderByDescending(s => s.VehicleOwnerName);
                    break;

                default:  // Name ascending 
                    Contacts = Contacts.OrderBy(s => s.VehicleOwnerName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Contacts.ToPagedList(pageNumber, pageSize));
        }


        // GET: Vehicles/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Vehicle vehicle = db.Vehicles.Find(id);
        //    if (vehicle == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vehicle);
        //}

        // GET: Vehicles/Create
        [Authorize(Roles = "AdminView")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleId,VehicleRegistrationNumber,VehicleOwnerName,VehicleModel")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        [Authorize(Roles = "AdminView")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleId,VehicleRegistrationNumber,VehicleOwnerName,VehicleModel")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        [Authorize(Roles = "AdminView")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
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
