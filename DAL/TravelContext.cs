using Entity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TravelContext:IdentityDbContext
    {
        public static TravelContext db { get; set; }
        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<YemeIcme> YemeIcmeMekanlari { get; set; }
        public DbSet<Gezi> GeziMekanlari { get; set; }
        public TravelContext(): base ("DefaultConnection") { }
        public DbSet<Kullanici> Kullaniciler { get; set; }


        //public System.Data.Entity.DbSet<Entity.Models.Kullanici> Kullanicis { get; set; }
    }
}
