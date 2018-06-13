using Abb.SimpleChat.Business.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Abb.SimpleChat.External.RepositoryEntityFamework
{ 
    public class SimpleChatRepository<T> : IRepository<T>
        where T : class
    {
        SimpleChatContext context;
        DbSet<T> dbSet;
        public SimpleChatRepository(SimpleChatContext context)
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
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
