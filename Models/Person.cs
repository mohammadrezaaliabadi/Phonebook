using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class Person
    {
        public long Id { set; get; }
        public bool Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }

        public string Address { set; get; }

        public string Description { get; set; }
        public IFormFile Image { get; set; }

        public ICollection<Phone> Phones {get;set;}

    }
}
