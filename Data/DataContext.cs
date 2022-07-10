using Microsoft.EntityFrameworkCore;
using tracerapi.Models;

namespace tracerapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Intervention> Interventions { get; set; } 
        public DbSet<Tache> Taches { get; set; }
        public DbSet<Event> Events { get; set; }

    }
}
