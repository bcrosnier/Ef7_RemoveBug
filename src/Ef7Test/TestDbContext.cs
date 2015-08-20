using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Ef7Test
{
    public class TestDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactEmail> ContactEmails { get; set; }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            // Visual Studio 2015 | Use the LocalDb 12 instance created by Visual Studio
            optionsBuilder.UseSqlServer( @"Server=(localdb)\mssqllocaldb;Database=EFTest;Trusted_Connection=True;" );
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<Company>( entity =>
            {
                entity.Key( e => e.CompanyId );
                entity.Property( e => e.CompanyName ).Required();
            } );
            modelBuilder.Entity<Contact>( entity =>
            {
                entity.Key( e => e.ContactId );

                entity.Property( e => e.ContactName ).Required();
                entity.Property( e => e.CompanyId ).Required();

                entity.Reference( d => d.Company ).InverseCollection( p => p.Contacts ).ForeignKey( d => d.CompanyId ).Required();
            } );
            modelBuilder.Entity<ContactEmail>( entity =>
            {
                entity.Key( e => new { e.ContactId, e.ContactEmailAddress } );

                entity.Property( e => e.ContactId ).Required();
                entity.Property( e => e.ContactEmailAddress ).Required();

                entity.Reference( d => d.Contact ).InverseCollection( p => p.ContactEmails ).ForeignKey( d => d.ContactId );
            } );

        }

    }

    public class Company
    {
        public Company()
        {
            Contacts = new HashSet<Contact>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
    public class Contact
    {
        public Contact()
        {
            ContactEmails = new HashSet<ContactEmail>();
        }

        public int ContactId { get; set; }
        public int CompanyId { get; set; }
        public string ContactName { get; set; }

        public virtual ICollection<ContactEmail> ContactEmails { get; set; }
        public virtual Company Company { get; set; }
    }
    public class ContactEmail
    {
        public int ContactId { get; set; }
        public string ContactEmailAddress { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
