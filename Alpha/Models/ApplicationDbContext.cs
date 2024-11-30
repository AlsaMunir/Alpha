using Microsoft.EntityFrameworkCore;

namespace Alpha.Models { 
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Women> Women { get; set; }
        
        public DbSet<Men> Men { get; set; }
       
        public DbSet<SkinCare> SkinCare { get; set; }
       
        public DbSet<Accessories> Accessories { get; set; }


    }
}
