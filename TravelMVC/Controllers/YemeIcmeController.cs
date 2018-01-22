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
    public class YemeIcmeController : Controller
    {
        private TravelContext db = new TravelContext();

        // GET: YemeIcme
        public ActionResult Index()
        {
            return View(db.YemeIcmeMekanlari.ToList());
        }

        // GET: YemeIcme/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YemeIcme yemeIcme = db.YemeIcmeMekanlari.Find(id);
            if (yemeIcme == null)
            {
                return HttpNotFound();
            }
            return View(yemeIcme);
        }

        // GET: YemeIcme/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YemeIcme/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MekanID,MekanAd,MekanAdres,ResimURL,TelNo,Saatler,MAciklama")] YemeIcme yemeIcme, HttpPostedFileBase ResimURL, string sehir)
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
                    yemeIcme.ResimURL = dosyaAdi;
                }
                Sehir s = new Sehir();
                if (db.Sehirler.Where(u => u.SehirAd == sehir).Count() != 0)
                {
                    Sehir yeni = db.Sehirler.Where(x => x.SehirAd == sehir).FirstOrDefault();
                    yemeIcme.SehirAd = yeni;
                }
                else
                {
                    return View();

                }
                db.YemeIcmeMekanlari.Add(yemeIcme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yemeIcme);
        }

        // GET: YemeIcme/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YemeIcme yemeIcme = db.YemeIcmeMekanlari.Find(id);
            if (yemeIcme == null)
            {
                return HttpNotFound();
            }
            return View(yemeIcme);
        }

        // POST: YemeIcme/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MekanID,MekanAd,MekanAdres,ResimURL,TelNo,Saatler,MAciklama")] YemeIcme yemeIcme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yemeIcme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yemeIcme);
        }

        // GET: YemeIcme/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YemeIcme yemeIcme = db.YemeIcmeMekanlari.Find(id);
            if (yemeIcme == null)
            {
                return HttpNotFound();
            }
            return View(yemeIcme);
        }

        // POST: YemeIcme/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YemeIcme yemeIcme = db.YemeIcmeMekanlari.Find(id);
            db.YemeIcmeMekanlari.Remove(yemeIcme);
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
