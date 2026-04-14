using ExamenLinguaMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamenLinguaMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ExamenLinguaMVC.Models.Curso> Curso { get; set; } = default!;
        public DbSet<ExamenLinguaMVC.Models.Horario> Horario { get; set; } = default!;
        public DbSet<ExamenLinguaMVC.Models.Instructor> Instructor { get; set; } = default!;
        public DbSet<ExamenLinguaMVC.Models.MaterialDidactico> MaterialDidactico { get; set; } = default!;
        public DbSet<ExamenLinguaMVC.Models.Nivel> Nivel { get; set; } = default!;
    }
}
