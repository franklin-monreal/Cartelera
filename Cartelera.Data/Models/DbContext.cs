using Microsoft.EntityFrameworkCore;

namespace Cartelera.Data
{
    public class MyContext : DbContext
    {


        public DbSet<Pelicula> Peliculas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "CarteleraDB.db");
            //optionsBuilder.UseSqlite("Data Source=" + path + ";");
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
