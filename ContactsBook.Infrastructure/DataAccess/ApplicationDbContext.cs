using ContactsBook.Domain.Core;
using ContactsBook.Domain.Entities.Contacts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ContactsBook.Infrastructure.DataAccess
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}