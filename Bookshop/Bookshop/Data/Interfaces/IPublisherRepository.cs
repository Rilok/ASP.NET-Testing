using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookshop.Models;

namespace Bookshop.Data.Interfaces
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetPublishers();
        Task<IEnumerable<Publisher>> GetPublishers(string publisherName);
        Task<Publisher> GetPublisher(int publisherID);
        Task<bool> Save();
        bool PublisherExists(int publisherID);
        bool PublisherExists(Publisher publisher);
        void AddPublisher(Publisher publisher);
        void DeletePublisher(int publisherID);
        void UpdatePublisher(Publisher publisher);
    }
}
