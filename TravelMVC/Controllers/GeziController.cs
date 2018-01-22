using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entity.Models;

namespace TravelMVC.Controllers
{
    public class GeziController : Controller
    {
        private TravelContext db = new TravelContext();

        // GET: Gezi
        public ActionResult Index()
        {
            return View(db.GeziMekanlari.ToList());
        }

        // GET: Gezi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gezi gezi = db.GeziMekanlari.Find(id);
            if (gezi == null)
            {
                return HttpNotFound();
            }
            return View(gezi);
        }

        // GET: Gezi/Create
        public ActionResult Create()
        {
            foreach (var item in db.Sehirler)
            {
                ViewBag.Sehirler = item.SehirAd;
            }
            return View();
        }

        // POST: Gezi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MekanID,MekanAd,MekanAdres,ResimURL,TelNo,Saatler,MAciklama")] Gezi gezi, HttpPostedFileBase ResimURL, string sehir)
        {


            if (ModelState.IsValid)
            {
                var klasor = Server.MapPath("/Content/uploads/");
                if (ResimURL != null && ResimURL.ContentLength != 0)
                {
                    FileInfo fi = new FileInfo(ResimURL.FileName);
                    var rastgele = Guid.NewGuid().ToString().Substring(0, 5);
                    var dosyaAdi = fi.Name + rastgele + fi.Extension;
                    ResimURL.SaveAs(klasor + dosyaAdi);
                    gezi.ResimURL = dosyaAdi;
                }

                Sehir s = new Sehir();
                if (db.Sehirler.Where(u => u.SehirAd == sehir).Count() != 0)
                {
                    Sehir yeni = db.Sehirler.Where(x => x.SehirAd == sehir).FirstOrDefault();
                    gezi.SehirAd = yeni;
                }
                else
                {
                    return View();

                }

                db.GeziMekanlari.Add(gezi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gezi);
        }

        // GET: Gezi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gezi gezi = db.GeziMekanlari.Find(id);
            if (gezi == null)
            {
                return HttpNotFound();
            }
            return View(gezi);
        }

        // POST: Gezi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MekanID,MekanAd,MekanAdres,ResimURL,TelNo,Saatler,MAciklama")] Gezi gezi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gezi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gezi);
        }

        // GET: Gezi/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gezi gezi = db.GeziMekanlari.Find(id);
            if (gezi == null)
            {
                return HttpNotFound();
            }
            return View(gezi);
        }

        // POST: Gezi/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gezi gezi = db.GeziMekanlari.Find(id);
            db.GeziMekanlari.Remove(gezi);
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
