using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteleraApi
{
    public class MyContext : DbContext
    {
        public DbSet<Pelicula> Peliculas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=./CarteleraDB.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pelicula>().ToTable("Peliculas")
             .Property(e => e.Id)
             .ValueGeneratedOnAdd();
            SQLitePCL.Batteries.Init();

        }
    }
}
