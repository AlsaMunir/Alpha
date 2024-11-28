using Microsoft.EntityFrameworkCore;

namespace Alpha.Models  // Change this to match your namespace
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // Define DbSets (tables in the database)
        public DbSet<Women> Women { get; set; }
        // Define DbSets (tables in the database)
        public DbSet<Men> Men { get; set; }
        // Define DbSets (tables in the database)
        public DbSet<SkinCare> SkinCare { get; set; }
        // Define DbSets (tables in the database)
        public DbSet<Accessories> Accessories { get; set; }


    }
}
