using Microsoft.EntityFrameworkCore;

namespace BookMessenger.Models
{
    public class ApplicationContext :DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<Discuss> Discusses { get; set; }  
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserBook> Marks { get; set; }  
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<AdminActionLog> AdminActions { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Author>()
                .HasMany(c => c.Books)
                .WithMany(s => s.Authors)
                .UsingEntity<AuthorBook>(
                   a => a
                    .HasOne(pt => pt.Book)
                    .WithMany(t => t.AuthorBooks)
                    .HasForeignKey(pt => pt.BookId),
                   a => a
                    .HasOne(pt => pt.Author)
                    .WithMany(p => p.AuthorBooks)
                    .HasForeignKey(pt => pt.AuthorId)
               );

            modelBuilder
                .Entity<UserProfile>()
                .HasMany(c => c.Books)
                .WithMany(s => s.UserProfiles)
                .UsingEntity<UserBook>(
                   a => a
                    .HasOne(pt => pt.Book)
                    .WithMany(t => t.UserBooks)
                    .HasForeignKey(pt => pt.BookId),
                   a => a
                    .HasOne(pt => pt.User)
                    .WithMany(p => p.UserBooks)
                    .HasForeignKey(pt => pt.UserProfileId)
               );
        }
    }
}
