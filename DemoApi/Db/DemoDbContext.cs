using DemoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DemoApi.Db
{
    public class DemoDbContext:DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
     

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();
            user.HasKey(x => x.Id);


           

            var address= modelBuilder.Entity<UserAddress>();
            address.HasKey(x => x.Id);
            address.HasOne(ad => ad.User).WithOne(us => us.UserAddres).HasForeignKey<UserAddress>(us=>us.UserId);



            var bank = modelBuilder.Entity<BankAccount>();
            bank.HasKey(x => x.Id);
            bank.HasOne(ad => ad.User).WithMany(ad => ad.BankAccounts).HasForeignKey(z=>z.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
