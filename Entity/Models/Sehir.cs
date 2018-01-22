using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Sehir
    {
        public Sehir()
        {
            this.YemeIcmeList = new HashSet<YemeIcme>();
            this.GeziList = new HashSet<Gezi>();
        }
        [Key]
        public int SehirID { get; set; }
        public string SehirAd { get; set; }
        public Bölgeler SehirBölge { get; set; }
        public string Aciklama { get; set; }
        public string ResimURL { get; set; }
        public virtual Kullanici Kullanicikcik { get; set; }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YemeIcme> YemeIcmeList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gezi> GeziList { get; set; }
        

        public enum Bölgeler
        {
            Ege,
            Akdeniz,
            İçAnadolu,
            Marmara,
            Karadeniz,
            GüneydoğuAnadolu,
            DoğuAnadolu,
        }
    }
}
