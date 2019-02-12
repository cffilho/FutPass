using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Teste_de_aplicação.Models;
using System.Text;
using Teste_de_aplicação.Services;

namespace Teste_de_aplicação.Controllers
{
    [Authorize]
    public class ManagersController : Controller
    {
        private UEFAEntities db = new UEFAEntities();

        // GET: Managers
        public ActionResult Index()
        {
            var manager = db.Manager.Include(m => m.Club).Include(m => m.Country);
            return View(manager.ToList());
        }

     

        // GET: Managers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Manager.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            manager.Age = ManagersService.GetAge(manager);

            manager.GetImage = ManagersService.GetPhoto(manager);

            var model = manager.Country;
            Country country = model;
            manager.GetFlagCountry = CountryService.GetFlag(country);

            return View(manager);
        }

        // GET: Managers/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {

            ViewBag.ClubID = new SelectList(db.Club, "ClubID", "ClubName");
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName");
            return View();
        }

        // POST: Managers/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManagerID,FirstName,LastName,NickName,DateOfBirth,CountryID,ClubID,Photo")] Manager manager, HttpPostedFileBase photoCreate)
        {

           
            if (photoCreate != null) {
                manager.Photo = new byte[photoCreate.ContentLength];
                photoCreate.InputStream.Read(manager.Photo, 0, photoCreate.ContentLength);
            }

            if (ModelState.IsValid)
            {
               
                db.Manager.Add(manager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClubID = new SelectList(db.Club, "ClubID", "ClubName", manager.ClubID);
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", manager.CountryID);
            return View(manager);
        }


        // GET: Managers/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Manager.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClubID = new SelectList(db.Club, "ClubID", "ClubName", manager.ClubID);
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", manager.CountryID);
            return View(manager);
        }

        // POST: Managers/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManagerID,FirstName,LastName,NickName,DateOfBirth,CountryID,ClubID,Photo")] Manager manager, HttpPostedFileBase photoEdit)
        {

            if (photoEdit != null)
            {
                manager.Photo = new byte[photoEdit.ContentLength];
                photoEdit.InputStream.Read(manager.Photo, 0, photoEdit.ContentLength);
            }

            if (ModelState.IsValid)
            {
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClubID = new SelectList(db.Club, "ClubID", "ClubName", manager.ClubID);
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", manager.CountryID);
            return View(manager);
        }



        // GET: Managers/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Manager.Find(id);
            
            if (manager == null)
            {
                return HttpNotFound();
            }
            manager.GetImage = ManagersService.GetPhoto(manager);
            return View(manager);
        }

        // POST: Managers/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Manager.Find(id);
            db.Manager.Remove(manager);
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
