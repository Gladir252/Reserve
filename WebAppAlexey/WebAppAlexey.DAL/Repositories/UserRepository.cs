using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppAlexey.DAL.DataBaseContext;
using WebAppAlexey.DAL.Interfaces;
using WebAppAlexey.DAL.Models;

namespace WebAppAlexey.DAL.Repositories
{
    class UserRepository : IRepository<User>
    {
        readonly WebAppDataBaseContext db;
        private bool disposed;

        public UserRepository(WebAppDataBaseContext context)
        {
            db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.User;
        }

        public User Get(int id)
        {
            //return new User
            //{
            //    Id = 12,
            //    Email = "test@test"
            //};
            return db.User.Find(id);
        }
        public User GetByName(Func<User, bool> predicate)
        {
            return db.User.SingleOrDefault(predicate);
        }
        public void Create(User user)
        {
            db.User.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public IEnumerable<User> Find(Func<User, Boolean> predicate)
        {
            return db.User.Where(predicate).ToList();
        }

        public void Delete(int id, int? id2)
        {
            User user = db.User.Find(id);
            if (user != null)
                db.User.Remove(user);
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
