using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookshop.Data.Interfaces;
using Bookshop.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.Data.Repositories
{
    public class PublisherRepository : IPublisherRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public PublisherRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Publisher>> GetPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }
        public async Task<IEnumerable<Publisher>> GetPublishers(string publisherName)
        {
            return await _context.Publishers.Where(n => n.name.ToLower().Contains(publisherName.ToLower())).ToListAsync();
        }
        public async Task<Publisher> GetPublisher(int publisherID)
        {
            return await _context.Publishers.SingleOrDefaultAsync(x => x.id == publisherID);
        }

        public async Task<bool> Save()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PublisherExists(int publisherID)
        {
            return _context.Publishers.Any(e => e.id == publisherID);
        }

        public bool PublisherExists(Publisher publisher)
        {
            return _context.Publishers.Any(e => e.name == publisher.name &&
                                                e.street == publisher.street &&
                                                e.streetNumber == publisher.streetNumber &&
                                                e.postalCode == publisher.postalCode &&
                                                e.city == publisher.city &&
                                                e.phone == publisher.phone);
        }

        public void AddPublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
        }

        public void DeletePublisher(int publisherID)
        {
            Publisher publisher = _context.Publishers.Find(publisherID);
            _context.Publishers.Remove(publisher);
        }

        public void UpdatePublisher(Publisher publisher)
        {
            _context.Update(publisher);
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
