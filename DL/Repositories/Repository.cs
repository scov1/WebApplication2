using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext db = null;
        private readonly DbSet<T> table = null;
        public Repository()
        {
            db = new Model1();
            table = db.Set<T>();
        }

        public Repository(DbContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }
        public void Create(T item)
        {
            table.Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T FullEntity = table.Find(id);
            table.Remove(FullEntity);
        }

    }
}
