using ContactsBook.Domain.Entities.Contacts;
using Microsoft.EntityFrameworkCore;

namespace ContactsBook.Domain.Core
{
    public interface IApplicationDbContext
    {
        DbSet<Contact> Contacts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}