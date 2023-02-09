using ContactsBook.Domain.Entities.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsBook.Infrastructure.DataAccess.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(c => c.Email).IsRequired();

            builder.Property(c => c.PhoneNumber).IsRequired();
        }
    }
}