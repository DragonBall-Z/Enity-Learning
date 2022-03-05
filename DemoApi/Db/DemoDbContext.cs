using DemoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DemoApi.Db
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {


        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book_Author> Books_Authors { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();
            user.HasKey(x => x.Id);//PK
            user.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired(false);
           
            
            //One to One
            var address = modelBuilder.Entity<UserAddress>();
            address.HasKey(x => x.Id);//PK
            address.HasOne(ad => ad.User)
                   .WithOne(us => us.UserAddres)
                   .HasForeignKey<UserAddress>(us => us.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            //one to Many
            var bank = modelBuilder.Entity<BankAccount>();
            bank.HasKey(x => x.Id);//PK
            bank.HasOne(ad => ad.User)
                .WithMany(ad => ad.BankAccounts)
                .HasForeignKey(z => z.UserId);

            //many to Many

            var bookAuthor = modelBuilder.Entity<Book_Author>();
            bookAuthor.HasKey(k=>k.Id);
            bookAuthor.HasOne(ad => ad.Book)
                      .WithMany(a => a.Book_Authors)
                      .HasForeignKey(fk=>fk.BookId);

            bookAuthor.HasKey(k => k.Id);
            bookAuthor.HasOne(ad => ad.Author)
                      .WithMany(a => a.Book_Authors)
                      .HasForeignKey(fk => fk.AuthorId);



            base.OnModelCreating(modelBuilder);
        }
    }
}
