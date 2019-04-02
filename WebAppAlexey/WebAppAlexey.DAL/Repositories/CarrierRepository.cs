using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppAlexey.DAL.DataBaseContext;
using WebAppAlexey.DAL.Interfaces;
using WebAppAlexey.DAL.Models;

namespace WebAppAlexey.DAL.Repositories
{
    class CarrierRepository : IRepository<Carrier>
    {
        readonly WebAppDataBaseContext db;
        private bool disposed = false;

        public CarrierRepository(WebAppDataBaseContext context)
        {
            db = context;
        }
        public IEnumerable<Carrier> GetAll()
        {
            return db.Carrier.AsEnumerable();
        }

        public Carrier Get(int id)
        {
            return db.Carrier.Find(id);
        }
        public Carrier GetByName(Func<Carrier, Boolean> predicate)
        {
            return db.Carrier.FirstOrDefault(predicate);
        }

        public void Create(Carrier carrier)
        {
            db.Carrier.Add(carrier);
        }

        public void Update(Carrier carrier)
        {
            db.Entry(carrier).State = EntityState.Modified;
        }

        public IEnumerable<Carrier> Find(Func<Carrier, Boolean> predicate)
        {
            return db.Carrier.Where(predicate).ToList();
        }

        public void Delete(int id, int? id2)
        {
            Carrier carrier = db.Carrier.Find(id);
            if (carrier != null)
                db.Carrier.Remove(carrier);
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

