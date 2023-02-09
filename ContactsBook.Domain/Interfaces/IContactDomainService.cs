using ContactsBook.Domain.Entities.Contacts;

namespace ContactsBook.Domain.Interfaces
{
    public interface IContactDomainService
    {
        Task<List<Contact>> GetAsync(CancellationToken cancellationToken);
        Task<Contact> CreateAsync(Contact entity, CancellationToken cancellationToken);
    }
}