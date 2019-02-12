using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Teste_de_aplicação.Models;
using Teste_de_aplicação.Services;

namespace Teste_de_aplicação.Controllers
{
    [Authorize]
    public class ClubsController : Controller
    {
        private UEFAEntities db = new UEFAEntities();
        

        // GET: Clubs
        public ActionResult Index()
        {
            var club = db.Club.Include(c => c.Country);
            return View(club.ToList());
        }

        // GET: Clubs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Club.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }

            club.GetLogo = ClubsService.GetPhoto(club);

            var model = club.Country;
            Country country = model;
            club.GetFlagCountry = CountryService.GetFlag(country);

            return View(club);
        }

        // GET: Clubs/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName");
            return View();
        }

        // POST: Clubs/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClubID,ClubName,ClubFullName,CountryID,FoundationDate,CityClub,PriceMerch,ImageShield,ClubHistory,President,Stadium,WebSite")] Club club, HttpPostedFileBase photoCreate)
        {
            if (photoCreate != null)
            {
                club.ImageShield = new byte[photoCreate.ContentLength];
                photoCreate.InputStream.Read(club.ImageShield, 0, photoCreate.ContentLength);
            }

            if (ModelState.IsValid)
            {
                db.Club.Add(club);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", club.CountryID);
            return View(club);
        }

        // GET: Clubs/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Club.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", club.CountryID);
            return View(club);
        }

        // POST: Clubs/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClubID,ClubName,ClubFullName,CountryID,FoundationDate,CityClub,PriceMerch,ImageShield,ClubHistory,President,Stadium,WebSite")] Club club, HttpPostedFileBase imageEdit)
        {
            if (imageEdit != null)
            {
                club.ImageShield = new byte[imageEdit.ContentLength];
                imageEdit.InputStream.Read(club.ImageShield, 0, imageEdit.ContentLength);
            }
            if (ModelState.IsValid)
            {
                db.Entry(club).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", club.CountryID);
            return View(club);
        }

        // GET: Clubs/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Club.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            club.GetLogo = ClubsService.GetPhoto(club);
            return View(club);
        }

        // POST: Clubs/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Club club = db.Club.Find(id);
            db.Club.Remove(club);
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
