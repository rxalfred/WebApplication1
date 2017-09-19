using ContosoUniversity.Model.CoreTestModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.DataAccessLayer.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(DbContext context)
        {
            this.context = context as SchoolContext;
            entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> Find<TKey>(TKey id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<T> Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await context.SaveChangesAsync();
            return entity;
        }

        public async void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}