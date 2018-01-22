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
    public class SehirController : Controller
    {
        private TravelContext db = new TravelContext();

        // GET: Sehir
        public ActionResult Index()
        {
            return View(db.Sehirler.ToList());
        }

        // GET: Sehir/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sehir sehir = db.Sehirler.Find(id);
            if (sehir == null)
            {
                return HttpNotFound();
            }
            return View(sehir);
        }

        // GET: Sehir/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sehir/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SehirID,SehirAd,SehirBölge,Aciklama,ResimURL")] Sehir sehir, HttpPostedFileBase ResimURL)
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
                    sehir.ResimURL = dosyaAdi;
                }            
                db.Sehirler.Add(sehir);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sehir);
        }

        // GET: Sehir/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sehir sehir = db.Sehirler.Find(id);
            if (sehir == null)
            {
                return HttpNotFound();
            }
            return View(sehir);
        }

        // POST: Sehir/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SehirID,SehirAd,SehirBölge,Aciklama,ResimURL")] Sehir sehir)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sehir).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sehir);
        }

        // GET: Sehir/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sehir sehir = db.Sehirler.Find(id);
            if (sehir == null)
            {
                return HttpNotFound();
            }
            return View(sehir);
        }

        // POST: Sehir/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sehir silinecekSehir = db.Sehirler.Where(x => x.SehirID == id).FirstOrDefault();
            if (silinecekSehir.GeziList.Count!=0||silinecekSehir.YemeIcmeList.Count!=0)
            {
                foreach (var item in silinecekSehir.GeziList.ToList())
                {
                    silinecekSehir.GeziList.Remove(item);
                }
                foreach (var item in silinecekSehir.YemeIcmeList.ToList())
                {
                    silinecekSehir.YemeIcmeList.Remove(item);
                }
            }
            db.Sehirler.Remove(silinecekSehir);
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
