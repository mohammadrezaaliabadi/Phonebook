using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PhoneBookContext>
    {

        PhoneBookContext IDesignTimeDbContextFactory<PhoneBookContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<PhoneBookContext>();
           // var connectionString = configuration.GetConnectionString("Data Source=DESKTOP-D0S3D9C;Initial Catalog=PhoneBook;Integrated Security=True");
            builder.UseSqlServer("Data Source=DESKTOP-D0S3D9C;Initial Catalog=PhoneBook;Trusted_Connection=True");
            //builder.UseSqlServer("Data Source=DESKTOP-D0S3D9C;Initial Catalog=PhoneBook;Integrated Security=True");
            return new PhoneBookContext(builder.Options);
        }
    }
}
