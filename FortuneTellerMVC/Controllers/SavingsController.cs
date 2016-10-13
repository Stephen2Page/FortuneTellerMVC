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
    public class SavingsController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Savings
        public ActionResult Index()
        {
            return View(db.Savings.ToList());
        }

        // GET: Savings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saving saving = db.Savings.Find(id);
            if (saving == null)
            {
                return HttpNotFound();
            }
            return View(saving);
        }

        // GET: Savings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Savings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SavingsID,SavingsAmount")] Saving saving)
        {
            if (ModelState.IsValid)
            {
                db.Savings.Add(saving);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saving);
        }

        // GET: Savings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saving saving = db.Savings.Find(id);
            if (saving == null)
            {
                return HttpNotFound();
            }
            return View(saving);
        }

        // POST: Savings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SavingsID,SavingsAmount")] Saving saving)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saving).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saving);
        }

        // GET: Savings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saving saving = db.Savings.Find(id);
            if (saving == null)
            {
                return HttpNotFound();
            }
            return View(saving);
        }

        // POST: Savings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Saving saving = db.Savings.Find(id);
            db.Savings.Remove(saving);
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
