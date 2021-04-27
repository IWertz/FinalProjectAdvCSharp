using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectGamesWebApp.Models
{
    //repository interface, created for standardized repository pattern
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> List(QueryOptions<T> options);
        T Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
