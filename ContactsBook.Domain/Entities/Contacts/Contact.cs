namespace ContactsBook.Domain.Entities.Contacts
{
    public class Contact
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }

        public void GenerateId() => Id = Guid.NewGuid();

        public Contact(Guid id, string fullName, string email, string phoneNumber, string address)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}