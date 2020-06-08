using Microsoft.EntityFrameworkCore;
using System;

namespace ChessBuild.DAL
{
    public class ChessContext : DbContext
    {
        public virtual DbSet<DomainClasses.User> Users { get; set; }
        public virtual DbSet<DomainClasses.Moves> Moves { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-PPMFOVU;Database=ChessBuildDB;Trusted_Connection=True;");
        }
    }
}
