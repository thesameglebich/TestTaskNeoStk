using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskNeoStk.DataAccessLayer.Models;

namespace TestTaskNeoStk.DataAccessLayer
{
    public class DB: DbContext
    {
        public DB(DbContextOptions options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
