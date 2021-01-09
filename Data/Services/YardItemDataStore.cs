﻿using Core;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Services
{
    public class YardItemDataStore
        : IDataStore<YardItem>
    {
        protected readonly List<YardItem> _items;
        protected readonly HashStack<YardItem> _recentItems = new HashStack<YardItem>();
        public YardItemDataStore()
        {
            _items = new List<YardItem>()
            {
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = BoatClass.Falcon, Owner = "Bob Jones" },
                new YardItem(Guid.Parse("58de5eac-5d74-49d8-91b0-3fb148c58b39")) { Zone = "B13", BoatClass = BoatClass.Laser, Owner = "Rob Jones", DueDate = DateTime.Now.AddDays(1) },
                new YardItem() { Zone = "C8", BoatClass = BoatClass.GP14, Owner = "Paul Jones", DueDate = DateTime.Now.AddDays(1) },
                new YardItem() { Zone = "D2", BoatClass = BoatClass.Falcon, Owner = "Jeff Jones", DueDate = DateTime.Now.AddDays(1) },
                new YardItem() { Zone = "E2", BoatClass = BoatClass.GP14, Owner = "Obi Wan Jones", DueDate = DateTime.Now.AddDays(1) },
                new YardItem() { Zone = "F12", BoatClass = BoatClass.Laser, Owner = "Indiana Jones" }
            };

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

        public async Task<IEnumerable<YardItem>> GetRecentItemsAsync()
        {
            return await Task.FromResult(_recentItems);
        }

        public void AddRecentItem(YardItem item)
        {
            _recentItems.Push(item);
        }

        public void DeleteRecentItem(Guid id)
        {            
            var item = _recentItems.Where(x => x.Id == id);

            if (item == null) return;

            if (!item.Any()) return;

            _recentItems.Remove(item.First());
        }

        public Task<bool> UpdateRecentItemAsync(YardItem item)
        {
            return Task.Run(() =>
            {
                var found = _recentItems.FirstOrDefault(x => x.Id == item.Id);

                if (found != null)
                {
                    found = item;
                    return true;
                }
                else
                    return false;                
            });
        }
    }
}