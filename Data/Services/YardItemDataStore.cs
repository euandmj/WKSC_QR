using Core.Collections;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Services
{
    public class YardItemDataStore
        : IDataStore<YardItem, Guid>
        // TODO: align this new idatastore with mobile refactor
    {
        //protected readonly List<YardItem> _items;
        protected readonly HashStack<YardItem> _recentItems = new HashStack<YardItem>();

        public event NewDataEvent<YardItem> Refreshed;

        public ObservableCollection<YardItem> Items => throw new NotImplementedException();

        public YardItemDataStore()
        {
            //_items = new List<YardItem>()
            //{
            //    new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = BoatClass.Falcon, Owner = "Bob Jones" },
            //    new YardItem(Guid.Parse("58de5eac-5d74-49d8-91b0-3fb148c58b39")) { Zone = "B13", BoatClass = BoatClass.Laser, Owner = "Rob Jones", DueDate = DateTime.Now.AddDays(1) },
            //    new YardItem(Guid.Parse("82253571-c5f4-42f2-81e3-20aaa0f3551c")) { Zone = "C8", BoatClass = BoatClass.GP14, Owner = "Paul Jones" },
            //    new YardItem(Guid.Parse("b8ae8d13-fd78-460b-ab83-e26ac0540afe")) { Zone = "D2", BoatClass = BoatClass.Falcon, Owner = "Jeff Jones", DueDate = DateTime.Now.AddDays(16) },
            //    new YardItem() { Zone = "E2", BoatClass = BoatClass.GP14, Owner = "Obi Wan Jones", DueDate = DateTime.Now.AddDays(1) },
            //    new YardItem() { Zone = "F12", BoatClass = BoatClass.Laser, Owner = "Indiana Jones" }
            //};

#if DEBUG

            _recentItems.Push(new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "Falcon", Owner = "Bob Jones" });
            _recentItems.Push(new YardItem(Guid.Parse("82253571-c5f4-42f2-81e3-20aaa0f3551c")) { Zone = "C8", BoatClass = "GP14", Owner = "Paul Jones" });
            _recentItems.Push(new YardItem(Guid.Parse("b8ae8d13-fd78-460b-ab83-e26ac0540afe")) { Zone = "D2", BoatClass = "Falcon", Owner = "Jeff Jones", DueDate = DateTime.Now.AddDays(16) });

#endif
        }    

        public YardItem GetItem(Guid id)
        {
            return _recentItems.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<YardItem> GetItems()
        {
            // Todo: if forceRefresh then reload from database.
            return _recentItems;
        }

        public void AddItem(YardItem item)
        {
            _recentItems.Push(item);
        }

        public void DeleteItem(Guid id)
        {            
            var item = _recentItems.Where(x => x.Id == id);

            if (item == null) return;

            if (!item.Any()) return;

            _recentItems.Remove(item.First());
        }

        public void RefreshStore()
        {
            throw new NotImplementedException();
        }

        public async Task<YardItem> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<YardItem>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<YardItem>> GetItemsAsync(Func<YardItem, bool> filter)
        {
            throw new NotImplementedException();
        }

        public YardItem GetItem()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<YardItem> GetItems(Func<YardItem, bool> filter)
        {
            throw new NotImplementedException();
        }

        //public Task<bool> UpdateRecentItemAsync(YardItem item)
        //{
        //    return Task.Run(() =>
        //    {
        //        var found = _recentItems.FirstOrDefault(x => x.Id == item.Id);

        //        if (found != null)
        //        {
        //            found = item;
        //            return true;
        //        }
        //        else
        //            return false;                
        //    });
        //}
    }
}
