using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class PhonesController : Controller
    {
        private readonly PhoneBookContext _context;

        public PhonesController (PhoneBookContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(long? pid)
        {
            if (pid == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FirstOrDefaultAsync<Person>(p => p.Id == pid);
            
            if(person == null)
            {
                return NotFound();
            }

           
            var phones = _context.Phones.Where(p => p.PersonId == pid).ToList<Phone>();

            var personPehones = new PhoneBookIndexViewModel { Person = person, Phones = phones };
           

            return View(personPehones);
        }

        // GET: Phones/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = phone.PersonId;

            return View(phone);
        }


        public async Task<IActionResult> Add(long? pid)
        {
            if(pid == null)
            {
                return NotFound();
            }


            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == pid);
            if (person == null)
            {
                return NotFound();
            }

            ViewData["PersonId"] = person.Id;

            return View();
        }

        private object List<T>()
        {
            throw new NotImplementedException();
        }

        // POST: PersonPhone/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Type,Number,PersonId")] Phone phone)
        {


            if (ModelState.IsValid)
            {
                /*if (phone.PersonId != null)
                {
                    Person person = _context.Persons.First(p => p.Id == phone.Person.Id);

                    phone.Person = person;
                }*/


                _context.Add(phone);
                await _context.SaveChangesAsync();

                return new RedirectToActionResult(nameof(Index), "Phones", new {pid =  phone.PersonId});
            }
            ViewData["PersonId"] = phone.PersonId;
            return View(phone);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = phone.PersonId;
            return View(phone);
        }

        // POST: Phones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Type,Number,PersonId")] Phone phone)
        {
            if (id != phone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneExists(phone.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return new RedirectToActionResult(nameof(Index), "Phones", new { pid = phone.PersonId });
            }
            ViewData["PersonId"] = phone.PersonId;
            return View(phone);
        }

        // GET: PersonPhone/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            ViewData["PersonId"] = phone.PersonId;

            return View(phone);
        }

        // POST: PersonPhone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var phone = await _context.Phones.FindAsync(id);
            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();
            return new RedirectToActionResult(nameof(Index), "Phones", new { pid = phone.PersonId });

        }
        private bool PhoneExists(long id)
        {
            return _context.Phones.Any(e => e.Id == id);
        }
    }
}