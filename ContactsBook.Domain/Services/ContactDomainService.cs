using ContactsBook.Domain.Core;
using ContactsBook.Domain.Entities.Contacts;
using ContactsBook.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactsBook.Domain.Services
{
    public class ContactDomainService : IContactDomainService
    {
        public readonly IApplicationDbContext _contactsContext;

        public ContactDomainService(IApplicationDbContext contactsContext)
        {
            _contactsContext = contactsContext;
        }

        public async Task<List<Contact>> GetAsync(CancellationToken cancellationToken)
        {
            var contacts = await _contactsContext.Contacts.ToListAsync(cancellationToken);

            return contacts;
        }

        public async Task<Contact> CreateAsync(Contact contact, CancellationToken cancellationToken)
        {
            if (contact.Id == Guid.Empty)
            {
                contact.GenerateId();
            }

            if (await _contactsContext.Contacts.AnyAsync(x => x.Id == contact.Id, cancellationToken))
                throw new Exception("Contact with similar id already exists");

            var result = await _contactsContext.Contacts.AddAsync(contact, cancellationToken);

            await _contactsContext.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }
    }
}