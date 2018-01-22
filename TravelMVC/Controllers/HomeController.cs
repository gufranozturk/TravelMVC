using System.Web.Mvc;
using BLL;
using DAL;

namespace TravelMVC.Controllers
{
    public class HomeController : Controller
    {
        
        TravelContext db = new TravelContext();
        SehirRepository sRep = new SehirRepository();
        YemeIcmeRepository yRep = new YemeIcmeRepository();
        GeziRepository gRep = new GeziRepository();
        public ActionResult Anasayfa()
        {                        
            return View(sRep.GetAll());
        }
        public ActionResult SehirDetay(int id)
        {            
            return View(new SehirRepository().GetByID(id));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AnasayfaListe()
        {            
            return View(sRep.GetAll());
        }

        public ActionResult SehirDetayYeni()
        {
            return View();
        }

        public ActionResult CreateAll()
        {
            return View();
        }
        public ActionResult YemeIcmeDetay()
        {
            return View(yRep.GetAll());
        }
        public ActionResult GeziDetay()
        {
            return View(gRep.GetAll());
        }
    }
}