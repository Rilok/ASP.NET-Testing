using System;
using System.Threading.Tasks;
using Bookshop.Data.Interfaces;
using Bookshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }
        public async Task<IActionResult> Index(string search)
        {
            if (!String.IsNullOrEmpty(search))
                return View(await _publisherRepository.GetPublishers(search));
            else
                return View(await _publisherRepository.GetPublishers());
        }
        // Dodawanie wydawcy - GET
        public IActionResult Create()
        {
            return View();
        }
        // Dodawanie wydawcy - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _publisherRepository.AddPublisher(publisher);
                await _publisherRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(publisher);
        }
        // Szczegóły wydawcy
        public async Task<IActionResult> Details(int id)
        {
            var publisher = await _publisherRepository.GetPublisher(id);
            if (publisher == null)
            {
                return View("NotFound");
            }

            return View(publisher);
        }
        // Edytowanie wydawcy
        public async Task<IActionResult> Edit(int id)
        {
            var publisher = await _publisherRepository.GetPublisher(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Publisher publisher)
        {
            if (id != publisher.id)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _publisherRepository.UpdatePublisher(publisher);
                    await _publisherRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_publisherRepository.PublisherExists(publisher.id))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(publisher);
        }

        // Usuwanie wydawcy
        public async Task<IActionResult> Delete(int id)
        {
            var publisher = await _publisherRepository.GetPublisher(id);
            if (publisher == null)
            {
                return View("NotFound");
            }

            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePublisher(int id)
        {
            var publisher = await _publisherRepository.GetPublisher(id);
            _publisherRepository.DeletePublisher(id);
            await _publisherRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}