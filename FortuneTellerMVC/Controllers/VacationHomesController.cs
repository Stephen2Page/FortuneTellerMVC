using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC.Models;

namespace FortuneTellerMVC.Controllers
{
    public class VacationHomesController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: VacationHomes
        public ActionResult Index()
        {
            return View(db.VacationHomes.ToList());
        }

        // GET: VacationHomes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacationHome vacationHome = db.VacationHomes.Find(id);
            if (vacationHome == null)
            {
                return HttpNotFound();
            }
            return View(vacationHome);
        }

        // GET: VacationHomes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VacationHomes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VacationHomeID,VacationHome1")] VacationHome vacationHome)
        {
            if (ModelState.IsValid)
            {
                db.VacationHomes.Add(vacationHome);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vacationHome);
        }

        // GET: VacationHomes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacationHome vacationHome = db.VacationHomes.Find(id);
            if (vacationHome == null)
            {
                return HttpNotFound();
            }
            return View(vacationHome);
        }

        // POST: VacationHomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VacationHomeID,VacationHome1")] VacationHome vacationHome)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacationHome).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vacationHome);
        }

        // GET: VacationHomes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacationHome vacationHome = db.VacationHomes.Find(id);
            if (vacationHome == null)
            {
                return HttpNotFound();
            }
            return View(vacationHome);
        }

        // POST: VacationHomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VacationHome vacationHome = db.VacationHomes.Find(id);
            db.VacationHomes.Remove(vacationHome);
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
