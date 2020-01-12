using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Phonebook.Controllers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Models
{

    public class Phone
    {
        public long Id { set; get; }
        public PhoneType Type { set; get; }
        public long Number { get; set; }
        public long PersonId { set; get; }
        public Person Person { set; get; }

    }
}
