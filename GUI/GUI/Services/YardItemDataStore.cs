using GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUI.Services
{
    public class YardItemDataStore
        : IDataStore<YardItem>
    {
        protected readonly List<YardItem> _items;


        public YardItemDataStore()
        {
            _items = new List<YardItem>()
            {
                new YardItem('A', 2) { BoatClass = BoatClass.Falcon, Owner = "Bob Jones" },
                new YardItem('B', 2) { BoatClass = BoatClass.Laser, Owner = "Rob Jones", DueDate = DateTime.Now },
                new YardItem('C', 2) { BoatClass = BoatClass.GP14, Owner = "Paul Jones", DueDate = DateTime.Now },
                new YardItem('D', 2) { BoatClass = BoatClass.Falcon, Owner = "Jeff Jones", DueDate = DateTime.Now },
                new YardItem('E', 2) { BoatClass = BoatClass.GP14, Owner = "Obi Wan Jones", DueDate = DateTime.Now },
                new YardItem('F', 2) { BoatClass = BoatClass.Laser, Owner = "Indiana Jones" }
            };
            
        }



        public Task<bool> AddItemAsync(YardItem item)
        {
            // Todo: remove this from interface
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            // Todo: remove this from interface
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(YardItem item)
        {
            // Todo: remove this from interface
            throw new NotImplementedException();
        }

        public async Task<YardItem> GetItemAsync(Guid id)
        {
            return await Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
        }

        public async Task<IEnumerable<YardItem>> GetItemsAsync(bool forceRefresh = false)
        {
            // Todo: if forceRefresh then reload from database.
            return await Task.FromResult(_items);
        }
    }
}
