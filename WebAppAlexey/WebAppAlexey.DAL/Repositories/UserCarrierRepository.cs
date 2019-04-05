using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppAlexey.DAL.DataBaseContext;
using WebAppAlexey.DAL.Interfaces;
using WebAppAlexey.DAL.Models;

namespace WebAppAlexey.DAL.Repositories
{
    class UserCarrierRepository:IRepository<UserCarrier>
    {
        readonly WebAppDataBaseContext db;
        private bool disposed;

        public UserCarrierRepository(WebAppDataBaseContext context)
        {
            db = context;
        }

        public IEnumerable<UserCarrier> GetAll()
        {
            return db.UserCarrier;
        }

        public UserCarrier Get(int id)
        {
            return db.UserCarrier.Find(id);
        }
        public UserCarrier GetByName(Func<UserCarrier, Boolean> predicate)
        {
            return db.UserCarrier.FirstOrDefault(predicate);
        }
        public void Create(UserCarrier userCarrier)
        {
            db.UserCarrier.Add(userCarrier);
        }

        public void Update(UserCarrier userCarrier)
        {
            db.Entry(userCarrier).State = EntityState.Modified;
        }

        public IEnumerable<UserCarrier> Find(Func<UserCarrier, Boolean> predicate)
        {
            return db.UserCarrier.Where(predicate).ToList();
        }

        public void Delete(int id, int? id2)
        {
            UserCarrier userCarrier = db.UserCarrier.Find(id, id2);
            if (userCarrier != null)
                db.UserCarrier.Remove(userCarrier);
        }
        public void Save()
        {
            db.SaveChanges();
        }

        
        public virtual void Dispose(bool disposing)
        {
            if (disposed)
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
