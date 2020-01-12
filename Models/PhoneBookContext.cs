using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Person> Persons { set; get; }
        public virtual DbSet<Phone> Phones { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-D0S3D9C;Initial Catalog=PhoneBook;Trusted_Connection=True");
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-D0S3D9C;Initial Catalog=PhoneBook;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.Picture).HasColumnName("picture");
                entity.Property(e => e.Type).HasColumnName("type");
                entity.Property(e => e.Address).HasColumnName("address");
                entity.Property(e => e.Description).HasColumnName("description");
                
                entity.HasIndex(e => e.FirstName);
                entity.HasIndex(e => e.LastName);

                entity.Ignore(e => e.Image);
            });

            modelBuilder.Entity<Phone>(entity => 
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Type).HasColumnName("type");
                entity.Property(e => e.Number).HasColumnName("number");
                entity.Property(e => e.PersonId).HasColumnName("person_id");
               
                entity.HasIndex(e => e.Number).IsUnique();


                entity.HasOne<Person>(d => d.Person)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Phone_Person");
            });

        }


    }
}
