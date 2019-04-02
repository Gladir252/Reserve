using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppAlexey.DAL.Interfaces;
using WebAppAlexey.DAL.Models;
using WebAppAlexey.DAL.DataBaseContext;


namespace WebAppAlexey.DAL.Repositories
{
    class SubscriptionStatusRepository : IRepository<SubscriptionStatus>
    {
        readonly WebAppDataBaseContext db;
        private bool disposed = false;

        public SubscriptionStatusRepository(WebAppDataBaseContext context)
        {
            db = context;
        }
        public IEnumerable<SubscriptionStatus> GetAll()
        {
            return db.SubscriptionStatus;
        }

        public SubscriptionStatus Get(int id)
        {
            return db.SubscriptionStatus.Find(id);
        }
        public SubscriptionStatus GetByName(Func<SubscriptionStatus, Boolean> predicate)
        {
            return db.SubscriptionStatus.FirstOrDefault(predicate);
        }

        public void Create(SubscriptionStatus subscriptionStatus)
        {
            db.SubscriptionStatus.Add(subscriptionStatus);
        }

        public void Update(SubscriptionStatus subscriptionStatus)
        {
            db.Entry(subscriptionStatus).State = EntityState.Modified;
        }

        public IEnumerable<SubscriptionStatus> Find(Func<SubscriptionStatus, Boolean> predicate)
        {
            return db.SubscriptionStatus.Where(predicate).ToList();
        }

        public void Delete(int id, int? id2)
        {
            SubscriptionStatus subscriptionStatus = db.SubscriptionStatus.Find(id);
            if (subscriptionStatus != null)
                db.SubscriptionStatus.Remove(subscriptionStatus);
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
