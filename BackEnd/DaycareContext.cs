using Microsoft.EntityFrameworkCore;
using System;

namespace BackEnd
{
    public class DaycareContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-M2HEUQ01\SQLEXPRESS;
            Database=advSebastianGeidvik;Integrated Security=True;
            Connect Timeout=30;Encrypt=False;
            TrustServerCertificate=False;
            ApplicationIntent=ReadWrite;
            MultiSubnetFailover=False;
            MultipleActiveResultSets = True;").UseLazyLoadingProxies();
        }
        public DbSet<Hamster> Hamsters { get; set; }
        public DbSet<Cage> Cages { get; set; }
    }
}
