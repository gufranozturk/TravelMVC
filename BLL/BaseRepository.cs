using DAL;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace BLL
{
    public class BaseRepository<T> where T:class
    {
        public TravelContext db = new TravelContext();

        public List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }
        public T Find(Predicate<int> Condition)
        {
            return db.Set<T>().Find(Condition);
        }
        public void Insert(T newRecord)
        {
            db.Set<T>().Add(newRecord);
        }
        public void Delete(T Trash)
        {
            db.Set<T>().Remove(Trash);
            db.SaveChanges();
        }
        public void Update(T oldRecord)
        {
            db.SaveChanges();
        }
        public object GetByID(int ID)
        {
            return db.Sehirler.Find(ID);
        }
    }

    
    public class YemeIcmeRepository : BaseRepository<YemeIcme> { }
    public class GeziRepository : BaseRepository<Gezi> { }
    public class SehirRepository : BaseRepository<Sehir> { }
}
