using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookshop.Models;

namespace Bookshop.Data.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetStores();
        Task<Store> GetStore(int storeID);
        Task<bool> Save();
        bool storeExists(int storeID);
        bool storeExists(Store store);
        void AddStore(Store store);
        void DeleteStore(int storeID);
        void UpdateStore(Store store);
    }
}
