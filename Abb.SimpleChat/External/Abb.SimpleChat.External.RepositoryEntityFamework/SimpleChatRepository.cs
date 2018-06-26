using Abb.SimpleChat.Business.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Abb.SimpleChat.External.RepositoryEntityFamework
{ 
    public class SimpleChatRepository<T> : IRepository<T>
        where T : class
    {
        SimpleChatContext context;
        DbSet<T> dbSet;
        IDatabaseSettings databaseSettings;

        public SimpleChatRepository(IDatabaseSettings databaseSettings)
        {
            this.databaseSettings = databaseSettings;
            context = new SimpleChatContext(databaseSettings);
            dbSet = context.Set<T>();
            
        }

        public void CreatDatabase()
        {
            if (File.Exists(databaseSettings.ConnnectionString)) { }
            else context.Database.EnsureCreated();
        }

        public void ContextDispose()
        {
            context?.Dispose();
        }

        public int Count()
        {
            return dbSet.CountAsync().Result;
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
