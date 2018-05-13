using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookshop.Data.Interfaces;
using Bookshop.Models;

namespace Bookshop.Data.Fakes
{
    public class FakeStoreRepository : IStoreRepository
    {
        public List<Store> Stores = new List<Store>();

        public async Task<IEnumerable<Store>> GetStores()
        {
            return await Task.Run(() => Stores);
        }

        public async Task<Store> GetStore(int StoreId)
        {
            return await Task.Run(() => Stores.FirstOrDefault(x => x.id == StoreId));
        }

        public bool storeExists(int id)
        {
            return Stores.Any(e => e.id == id);
        }

        public bool storeExists(Store store)
        {
            return Stores.Any(e => e.amount == store.amount && 
                                   e.price == store.price);
        }

        public void AddStore(Store store)
        {
            Stores.Add(store);
        }

        public void DeleteStore(int storeId)
        {
            Store store = Stores.FirstOrDefault(x => x.id == storeId);
            Stores.Remove(store);
        }

        public void UpdateStore(Store store)
        {
            var s = Stores.FirstOrDefault(x => x.id == store.id);
            s = store;
        }

        public async Task<bool> Save()
        {
            return await Task.Run(() => true);
        }
    }
}
