using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookshop.Data;
using Bookshop.Data.Interfaces;
using Bookshop.Models;

namespace Bookshop.Controllers
{
    public class StoresController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IStoreRepository _storeRepository;
        
        public StoresController(IBookRepository bookRepository, 
            IPublisherRepository publisherRepository, IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
            _publisherRepository = publisherRepository;
            _bookRepository = bookRepository;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
            return View(await _storeRepository.GetStores());
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var store = await _storeRepository.GetStore(id);
            if (store == null)
            {
                return View("NotFound");
            }

            ViewData["BookID"] = new SelectList(await _bookRepository.GetBooks(), "id", "name");
            ViewData["PublisherID"] = new SelectList(await _publisherRepository.GetPublishers(), "id", "name");

            return View(store);
        }

        // GET: Stores/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BookID"] = new SelectList(await _bookRepository.GetBooks(), "id", "name");
            ViewData["PublisherID"] = new SelectList(await _publisherRepository.GetPublishers(), "id", "name");
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,amount,price,BookID,PublisherID")] Store store)
        {
            if (ModelState.IsValid)
            {
                _storeRepository.AddStore(store);
                await _storeRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(await _bookRepository.GetBooks(), "id", "name", store.BookID);
            ViewData["PublisherID"] = new SelectList(await _publisherRepository.GetPublishers(), "id", "name", store.PublisherID);
            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int id)
        { 
            var store = await _storeRepository.GetStore(id);
            if (store == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(await _bookRepository.GetBooks(), "id", "name", store.BookID);
            ViewData["PublisherID"] = new SelectList(await _publisherRepository.GetPublishers(), "id", "name", store.PublisherID);
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,amount,price,BookID,PublisherID")] Store store)
        {
            if (id != store.id)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _storeRepository.UpdateStore(store);
                    await _storeRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.id))
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
            ViewData["BookID"] = new SelectList(await _bookRepository.GetBooks(), "id", "name", store.BookID);
            ViewData["PublisherID"] = new SelectList(await _publisherRepository.GetPublishers(), "id", "name", store.PublisherID);
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var store = await _storeRepository.GetStore(id);
            if (store == null)
            {
                return View("NotFound");
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _storeRepository.GetStore(id);
            _storeRepository.DeleteStore(id);
            await _storeRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
            return _storeRepository.storeExists(id);
        }

        private bool StoreExists(Store store)
        {
            return _storeRepository.storeExists(store);
        }
    }
}
