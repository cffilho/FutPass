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
    public class PlayersController : Controller
    {
        private UEFAEntities db = new UEFAEntities();

        // GET: Players
        public ActionResult Index()
        {
            var player = db.Player.Include(p => p.Club).Include(p => p.Country).Include(p => p.PlayerStatus).Include(p => p.Position);
            return View(player.ToList());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Player.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }

            player.Age = PlayerService.GetAge(player);
            player.GetPhoto = PlayerService.GetPhoto(player);
            var model = player.Country;
            Country country = model;
            player.GetFlagCountry = CountryService.GetFlag(country);
            return View(player);
        }

        // GET: Players/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.ClubID = new SelectList(db.Club, "ClubID", "ClubName");
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName");
            ViewBag.PlayerStatusID = new SelectList(db.PlayerStatus, "PlayerStatusID", "NameStatus");
            ViewBag.PositionID = new SelectList(db.Position, "PositionID", "Position1");
            return View();
        }

        // POST: Players/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerID,FirstName,LastName,NickName,DateOfBirth,CountryID,PositionID,ClubID,PlayerStatusID,Photo,Appearances,YellowCard,RedCard,GoalsScored,Height,PricePass,Number")] Player player, HttpPostedFileBase photoCreate)
        {
            if (photoCreate != null)
            {
                player.Photo = new byte[photoCreate.ContentLength];
                photoCreate.InputStream.Read(player.Photo, 0, photoCreate.ContentLength);
            }

            if (ModelState.IsValid)
            {
                db.Player.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClubID = new SelectList(db.Club, "ClubID", "ClubName", player.ClubID);
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", player.CountryID);
            ViewBag.PlayerStatusID = new SelectList(db.PlayerStatus, "PlayerStatusID", "NameStatus", player.PlayerStatusID);
            ViewBag.PositionID = new SelectList(db.Position, "PositionID", "Position1", player.PositionID);
            return View(player);
        }

        // GET: Players/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Player.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClubID = new SelectList(db.Club, "ClubID", "ClubName", player.ClubID);
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", player.CountryID);
            ViewBag.PlayerStatusID = new SelectList(db.PlayerStatus, "PlayerStatusID", "NameStatus", player.PlayerStatusID);
            ViewBag.PositionID = new SelectList(db.Position, "PositionID", "Position1", player.PositionID);
            return View(player);
        }

        // POST: Players/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerID,FirstName,LastName,NickName,DateOfBirth,CountryID,PositionID,ClubID,PlayerStatusID,Photo,Appearances,YellowCard,RedCard,GoalsScored,Height,PricePass,Number")] Player player, HttpPostedFileBase photoEdit)
        {
            if (photoEdit != null)
            {
                player.Photo = new byte[photoEdit.ContentLength];
                photoEdit.InputStream.Read(player.Photo, 0, photoEdit.ContentLength);
            }

            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClubID = new SelectList(db.Club, "ClubID", "ClubName", player.ClubID);
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", player.CountryID);
            ViewBag.PlayerStatusID = new SelectList(db.PlayerStatus, "PlayerStatusID", "NameStatus", player.PlayerStatusID);
            ViewBag.PositionID = new SelectList(db.Position, "PositionID", "Position1", player.PositionID);
            return View(player);
        }

        // GET: Players/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Player.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            player.GetPhoto = PlayerService.GetPhoto(player);
            return View(player);
        }

        // POST: Players/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Player.Find(id);
            db.Player.Remove(player);
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
