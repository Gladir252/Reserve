using System;
using System.Collections.Generic;

namespace WebAppAlexey.DAL.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T GetByName(Func<T, Boolean> predicate);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id, int? id2);
        void Save();
    }
}
