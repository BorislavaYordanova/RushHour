using RushHour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RushHour.Repositories
{
    public interface IBaseRepository<T> : IDisposable  where T : BaseModel
    {
        IEnumerable<T> SelectAll();
        T SelectByID(int id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        void Save();
    }
}