using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookshop.Data;
using Bookshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.Controllers
{
    public class UsersController : Controller
    {

        private ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string option=null, string search=null)
        {
            var users = _db.Users.ToList();
            if (option == "email" && search != null)
            {
                users = _db.Users.Where(u => u.Email.ToLower().Contains(search.ToLower())).ToList();
            }
            else
            {
                if (option == "name" && search != null)
                {
                    users = _db.Users.Where(u => u.Name.ToLower().Contains(search.ToLower())).ToList();
                }
                else
                {
                    if (option == "surname" && search != null)
                    {
                        users = _db.Users.Where(u => u.Surname.ToLower().Contains(search.ToLower())).ToList();
                    }
                }
            }   
            return View(users);
        }
        // Szczegóły - GET
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // Edycja Użytkownika - GET
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Edycja użytkownika - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", user);
            }
            else
            {
                var userInDb = await _db.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
                if (userInDb == null)
                {
                    return NotFound();
                }
                else
                {
                    userInDb.Name = user.Name;
                    userInDb.Surname = user.Surname;

                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        // Usunięcie użytkownika - GET
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Usunięcie użytkownika - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userInDb = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);
            _db.Remove(userInDb);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}