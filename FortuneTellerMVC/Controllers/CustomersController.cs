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
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Saving).Include(c => c.Transportation).Include(c => c.VacationHome);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            if ((customer.Age % 2 > 0))
            {
                ViewBag.RetirementAge = 30;
            }
            else
            {
                ViewBag.RetirementAge = 15;
            }

            //vacation home based on siblings, five options, one based on range.
            
            if (customer.NumOfSiblings == 0)
            {
                customer.VacationHomeID = 1;
            }
            else if (customer.NumOfSiblings == 1)
            {
                customer.VacationHomeID = 2;
            }
            else if (customer.NumOfSiblings == 2)
            {
                customer.VacationHomeID = 3;
            }
            else if (customer.NumOfSiblings == 3)
            {
                customer.VacationHomeID = 4;
            }
            else if (customer.NumOfSiblings > 3)
            {
                customer.VacationHomeID = 5;
            }
            else
            {
                customer.VacationHomeID = 6;
            }

            //Transportation from color (seven options)
            string favColor;
            favColor = customer.FavColor.ToLower();
            favColor = favColor.Substring(0, 1);   //extract just first letter 
            switch (favColor)
            {
                case "r":
                    customer.TransportationID = 1;
                    break;

                case "o":
                    customer.TransportationID= 2;
                    break;

                case "y":
                    customer.TransportationID = 3;
                    break;

                case "g":
                    customer.TransportationID = 4;
                    break;

                case "b":
                    customer.TransportationID = 5;
                    break;

                case "i":
                    customer.TransportationID = 6;
                    break;

                case "v":
                    customer.TransportationID = 7;
                    break;

                default:
                    customer.TransportationID = 8;
                    break;

            }
            //Bank Account from Birth Month based on range. (four options)
            if (customer.BirthMonth >= 1 && customer.BirthMonth <= 4)
            {
                customer.SavingsID = 1;
            }
            else if (customer.BirthMonth >= 5 && customer.BirthMonth <= 8)
            {
                customer.SavingsID = 2;
            }
            else if (customer.BirthMonth >= 9 && customer.BirthMonth <= 12)
            {
                customer.SavingsID = 3;
            }
            else
            {
                customer.SavingsID = 4;
            }

            db.SaveChanges();
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.SavingsID = new SelectList(db.Savings, "SavingsID", "SavingsID");
            ViewBag.TransportationID = new SelectList(db.Transportations, "TransportationID", "Transportation1");
            ViewBag.VacationHomeID = new SelectList(db.VacationHomes, "VacationHomeID", "VacationHome1");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavColor,NumOfSiblings,VacationHomeID,TransportationID,SavingsID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SavingsID = new SelectList(db.Savings, "SavingsID", "SavingsID", customer.SavingsID);
            ViewBag.TransportationID = new SelectList(db.Transportations, "TransportationID", "Transportation1", customer.TransportationID);
            ViewBag.VacationHomeID = new SelectList(db.VacationHomes, "VacationHomeID", "VacationHome1", customer.VacationHomeID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.SavingsID = new SelectList(db.Savings, "SavingsID", "SavingsID", customer.SavingsID);
            ViewBag.TransportationID = new SelectList(db.Transportations, "TransportationID", "Transportation1", customer.TransportationID);
            ViewBag.VacationHomeID = new SelectList(db.VacationHomes, "VacationHomeID", "VacationHome1", customer.VacationHomeID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavColor,NumOfSiblings,VacationHomeID,TransportationID,SavingsID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SavingsID = new SelectList(db.Savings, "SavingsID", "SavingsID", customer.SavingsID);
            ViewBag.TransportationID = new SelectList(db.Transportations, "TransportationID", "Transportation1", customer.TransportationID);
            ViewBag.VacationHomeID = new SelectList(db.VacationHomes, "VacationHomeID", "VacationHome1", customer.VacationHomeID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
