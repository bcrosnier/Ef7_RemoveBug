using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ef7Test
{
    public class Program
    {
        public void Main(string[] args)
        {
            using( var db = new TestDbContext() )
            {
                Company company = new Company() { CompanyName = "Contoso" };

                Contact contact1 = new Contact() { ContactName = "Contact 1", Company = company };
                Contact contact2 = new Contact() { ContactName = "Contact 2", Company = company };
                Contact contact3 = new Contact() { ContactName = "Contact 3", Company = company };

                db.Add( company );
                db.Add( contact1 );
                db.Add( contact2 );
                db.Add( contact3 );

                db.SaveChanges();

                // Comment the next line to prevent null'ing the reference and throwing, but this will keep the old one in the parent's collections
                company.Contacts.Remove( contact2 );
                db.Contacts.Remove( contact2 );

                db.SaveChanges();

                if(company.Contacts.Contains(contact2))
                {
                    Console.WriteLine( "Removed entity still exists" );
                }

                Console.WriteLine( "Press Enter to exit" );
                Console.ReadLine();
            }
        }
    }
}
