using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppAlexey.DAL.DataBaseContext;
using WebAppAlexey.DAL.Interfaces;
using WebAppAlexey.DAL.Models;
/// <summary>
/// Coming soon...
/// </summary>
namespace WebAppAlexey.DAL.Repositories
{
    class UserCarrierRepository:IRepository<UserCarrier>
    {
        readonly WebAppDataBaseContext db;
        private bool disposed = false;

        public UserCarrierRepository(WebAppDataBaseContext context)
        {
            db = context;
        }

        public IEnumerable<UserCarrier> GetAll()
        {
            return db.UserCarrier;
        }

        public UserCarrier Get(int Id)
        {
            return db.UserCarrier.Find(Id);
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

        public void Delete(int Id, int? Id2)
        {
            UserCarrier userCarrier = db.UserCarrier.Find(Id, Id2);
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
