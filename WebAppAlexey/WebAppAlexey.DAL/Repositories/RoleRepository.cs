using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppAlexey.DAL.DataBaseContext;
using WebAppAlexey.DAL.Interfaces;
using WebAppAlexey.DAL.Models;

namespace WebAppAlexey.DAL.Repositories
{
    class RoleRepository : IRepository<Role>
    {
        readonly WebAppDataBaseContext db;
        private bool disposed = false;
        public RoleRepository(WebAppDataBaseContext context)
        {
            db = context;
        }
        public IEnumerable<Role> GetAll()
        {
            return db.Role;
        }

        public Role Get(int id)
        {
            return db.Role.Find(id);
        }
        public Role GetByName(Func<Role, Boolean> predicate)
        {
            return db.Role.FirstOrDefault(predicate);
        }

        public void Create(Role role)
        {
            db.Role.Add(role);
        }

        public void Update(Role role)
        {
            db.Entry(role).State = EntityState.Modified;
        }

        public IEnumerable<Role> Find(Func<Role, Boolean> predicate)
        {
            return db.Role.Where(predicate).ToList();
        }

        public void Delete(int id, int? id2)
        {
            Role role = db.Role.Find(id);
            if (role != null)
                db.Role.Remove(role);
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

