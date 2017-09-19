using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.DataAccessLayer.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Find<TKey>(TKey key);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        void Delete(T entity);
    }
}