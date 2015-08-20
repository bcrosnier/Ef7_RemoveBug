using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using Ef7Test;

namespace Ef7TestMigrations
{
    [ContextType(typeof(TestDbContext))]
    partial class InitialMigration
    {
        public override string Id
        {
            get { return "20150820164355_InitialMigration"; }
        }

        public override string ProductVersion
        {
            get { return "7.0.0-beta6-13815"; }
        }

        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815")
                .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

            builder.Entity("Ef7Test.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName")
                        .Required();

                    b.Key("CompanyId");
                });

            builder.Entity("Ef7Test.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<string>("ContactName")
                        .Required();

                    b.Key("ContactId");
                });

            builder.Entity("Ef7Test.ContactEmail", b =>
                {
                    b.Property<int>("ContactId");

                    b.Property<string>("ContactEmailAddress");

                    b.Key("ContactId", "ContactEmailAddress");
                });

            builder.Entity("Ef7Test.Contact", b =>
                {
                    b.Reference("Ef7Test.Company")
                        .InverseCollection()
                        .ForeignKey("CompanyId");
                });

            builder.Entity("Ef7Test.ContactEmail", b =>
                {
                    b.Reference("Ef7Test.Contact")
                        .InverseCollection()
                        .ForeignKey("ContactId");
                });
        }
    }
}
