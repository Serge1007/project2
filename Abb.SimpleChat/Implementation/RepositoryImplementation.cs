using Abb.SimpleChat.Business.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Abb.SimpleChat.External.Implementation
{
    public class RepositoryImplementation<T> : IRepository<T>
        where T : class
    {
        EntityContext context;
        DbSet<T> dbSet;

        

        public RepositoryImplementation(EntityContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public void Add(T item)
        {
            dbSet.Add(item);
        }

        public void DeleteItem(int id)
        {
            T entityToDelete = dbSet.Find(id);
            dbSet.Remove(entityToDelete);
        }

        public T GetItem(int id)
        {
            return dbSet.Find(id);
        }

        public void Update(T item)
        {
            dbSet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
