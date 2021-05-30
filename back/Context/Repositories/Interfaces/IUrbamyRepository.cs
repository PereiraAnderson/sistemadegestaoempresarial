using System.Collections.Generic;
using System.Linq;

namespace SGE.Context.Repositories.Interfaces
{
    public interface ISGERepository<T> where T : class
    {
        T Add(T model);
        void Add(IEnumerable<T> models);
        IQueryable<T> GetAll();
        T Get(dynamic key, IEnumerable<string> includes = null);
        T Update(T model);
        void Update(IEnumerable<T> models);
        T Remove(T model);
        void Remove(IEnumerable<T> models);
        T Remove(dynamic key);
        void Remove(IEnumerable<dynamic> keys);
        int SaveChanges();
    }
}