using GestiStage.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GestiStage
{
    public class GestiStageDbContext : DbContext
    {
    

        public static readonly string GestiStageDb = nameof(GestiStageDb).ToLower();
        public GestiStageDbContext(DbContextOptions<GestiStageDbContext> options) 
            :base(options)
        {
            Debug.WriteLine($"{ContextId} context created.");
        }
        
        public DbSet<Departement> Departements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departement>();

            base.OnModelCreating(modelBuilder);
        }
    }
}