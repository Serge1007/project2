using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Abb.SimpleChat.Business.Logic.Entities;

namespace Abb.SimpleChat.External.RepositoryEntityFamework
{
    public class SimpleChatContext : DbContext
    {
         IDatabaseSettings databaseSettings;

        public SimpleChatContext(IDatabaseSettings databaseSettings)
        {
            this.databaseSettings = databaseSettings ?? throw new ArgumentNullException(nameof(databaseSettings));
            CheckSettings();
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Roles> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"FileName={databaseSettings.ConnnectionString}");
        }

        private void CheckSettings()
        {
            if(databaseSettings==null) throw new ArgumentNullException(nameof(databaseSettings));
        }
    }
}
