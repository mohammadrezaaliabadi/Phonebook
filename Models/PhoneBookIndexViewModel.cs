using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class PhoneBookIndexViewModel
    {
        public Person Person { get; set; }

        public List<Phone> Phones { set; get; }

        public Phone Phone { set; get; }

    }
}
