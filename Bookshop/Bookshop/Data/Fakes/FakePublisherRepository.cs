using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookshop.Data.Interfaces;
using Bookshop.Models;

namespace Bookshop.Data.Fakes
{
    public class FakePublisherRepository : IPublisherRepository
    {
        public List<Publisher> Publishers = new List<Publisher>();

        public async Task<IEnumerable<Publisher>> GetPublishers()
        {
            return await Task.Run(() => Publishers);
        }

        public async Task<IEnumerable<Publisher>> GetPublishers(string publisherName)
        {
            return await Task.Run(() => Publishers.Where(x => x.name.ToLower().Contains(publisherName.ToLower())));
        }

        public async Task<Publisher> GetPublisher(int PublisherId)
        {
            return await Task.Run(() => Publishers.FirstOrDefault(x => x.id == PublisherId));
        }

        public bool PublisherExists(int id)
        {
            return Publishers.Any(e => e.id == id);
        }

        public bool PublisherExists(Publisher publisher)
        {
            return Publishers.Any(e => e.name == publisher.name);
        }

        public void AddPublisher(Publisher publisher)
        {
            Publishers.Add(publisher);
        }

        public void DeletePublisher(int publisherId)
        {
            Publisher publisher = Publishers.FirstOrDefault(x => x.id == publisherId);
            Publishers.Remove(publisher);
        }

        public void UpdatePublisher(Publisher publisher)
        {
            var p = Publishers.FirstOrDefault(x => x.id == publisher.id);
            p = publisher;
        }

        public async Task<bool> Save()
        {
            return await Task.Run(() => true);
        }
    }
}
