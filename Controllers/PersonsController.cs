using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class PersonsController : Controller
    {

        private readonly PhoneBookContext _context;

        public PersonsController(PhoneBookContext context)
        {
            this._context = context;
        }

        // GET: Persons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Persons.ToListAsync());
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FirstOrDefaultAsync<Person>(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        // GET: Persons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,FirstName,LastName,Image,Address,Description")]Person person)
        {
            if (ModelState.IsValid)
            {
                person.Picture = person.Image?.FileName;
                var Image = person.Image;
                person.Image = null;
                string ImageName = System.IO.Path.GetFileName(Image.FileName);
                var filePath = Path.Combine("C:\\Users\\Mohammad\\source\\repos\\Phonebook\\wwwroot\\images\\", ImageName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    Image.CopyTo(stream);
                }

                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(person);

        }

        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Persons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Type,FirstName,LastName,Image,Address,Description")] Person person)
        {
            if(id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);

        }

        private bool PersonExists(long id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = await _context.Persons.FirstOrDefaultAsync(m => m.Id == id);

            if(person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}