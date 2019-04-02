using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppAlexey.DAL.DataBaseContext;
using WebAppAlexey.DAL.Interfaces;
using WebAppAlexey.DAL.Models;

namespace WebAppAlexey.DAL.Repositories
{
    class AdressRepository : IRepository<Adress>
    {
        readonly WebAppDataBaseContext db;
        private bool disposed = false;

        public AdressRepository(WebAppDataBaseContext context)
        {
            db = context;
        }
        public IEnumerable<Adress> GetAll()
        {
            return db.Adress;
        }

        public Adress Get(int id)
        {
            return db.Adress.Find(id);
        }
        public Adress GetByName(Func<Adress, Boolean> predicate)
        {
            return db.Adress.FirstOrDefault(predicate);
        }
        public void Create(Adress adress)
        {
            db.Adress.Add(adress);
        }

        public void Update(Adress adress)
        {
            db.Entry(adress).State = EntityState.Modified;
        }

        public IEnumerable<Adress> Find(Func<Adress, Boolean> predicate)
        {
            return db.Adress.Where(predicate).ToList();
        }

        public void Delete(int id, int? id2)
        {
            Adress adress = db.Adress.Find(id);
            if (adress != null)
                db.Adress.Remove(adress);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
