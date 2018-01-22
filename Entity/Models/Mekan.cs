using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Mekan
    {
        [Key]
        public int MekanID { get; set; }
        public Sehir SehirAd { get; set; }
        public string MekanAd { get; set; }
        public string MekanAdres { get; set; }
        public string ResimURL { get; set; }
        public string TelNo { get; set; }
        public string Saatler { get; set; }
        public string MAciklama { get; set; }
       
    }
    public class YemeIcme : Mekan { }
    public class Gezi : Mekan { }
}
