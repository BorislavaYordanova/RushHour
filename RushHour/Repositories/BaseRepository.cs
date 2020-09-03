using RushHour.DataAccess;
using RushHour.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RushHour.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>  where T : BaseModel
    {
        protected RushHourContext db = null;
        private DbSet<T> _table = null;

        public BaseRepository()
        {
            this.db = new RushHourContext();
            _table = db.Set<T>();
        }

        public BaseRepository(RushHourContext db)
        {
            this.db = db;
            _table = db.Set<T>();
        }

        public IEnumerable<T> SelectAll()
        {
            return _table.ToList();
        }

        public T SelectByID(int id)
        {
            return _table.Find(id);
        }

        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(T obj)
        {
            _table.Remove(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
 
        }
        #endregion
    }
}