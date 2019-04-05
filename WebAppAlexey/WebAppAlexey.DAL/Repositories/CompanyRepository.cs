using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppAlexey.DAL.DataBaseContext;
using WebAppAlexey.DAL.Interfaces;
using WebAppAlexey.DAL.Models;

namespace WebAppAlexey.DAL.Repositories
{
    class CompanyRepository : IRepository<Company>
    {
        readonly WebAppDataBaseContext db;
        private bool disposed;

        public CompanyRepository(WebAppDataBaseContext context)
        {
            db = context;
        }
        public IEnumerable<Company> GetAll()
        {
            return db.Company;
        }

        public Company Get(int id)
        {
            return db.Company.Find(id);
        }
        public Company GetByName(Func<Company, Boolean> predicate)
        {
            return db.Company.FirstOrDefault(predicate);
        }

        public void Create(Company company)
        {
            db.Company.Add(company);
        }

        public void Update(Company company)
        {
            db.Entry(company).State = EntityState.Modified;
        }

        public IEnumerable<Company> Find(Func<Company, Boolean> predicate)
        {
            return db.Company.Where(predicate).ToList();
        }

        public void Delete(int id, int? id2)
        {
            Company company = db.Company.Find(id);
            if (company != null)
                db.Company.Remove(company);
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
