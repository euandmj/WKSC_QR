using Core.DependencyInjection;
using Core.Models;
using Data.Providers;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GUI.Providers
{
    class RecentlyEditedYardItemProvider
        : YardItemProviderBase
    {
        private readonly IDataStore<YardItem, string> _dataStore;
        private readonly int _recentNumber;

        public RecentlyEditedYardItemProvider(
            int numberRecent,
            Action<IEnumerable<YardItem>> onNewData)
        {
            _recentNumber = numberRecent;
            _dataStore = AutofacResolver.ResolveType<IDataStore<YardItem, string>>();


            _dataStore.Refreshed += (e, func) =>
            {
                onNewData(GetItems());
            };
        }

        public override YardItem GetItem(string x)
        {
            return _dataStore.GetItem(x);
        }

        public override IEnumerable<YardItem> GetItems()
        {
            return _dataStore
                .GetItems()
                .OrderByDescending(x => x.LastUpdated)
                .Take(_recentNumber);
        }

        public override IEnumerable<YardItem> GetItems(Func<YardItem, bool> filter)
        {
            return _dataStore
                .GetItems()
                .OrderByDescending(x => x.LastUpdated)
                .ThenByDescending(filter)
                .Take(_recentNumber);

        }
    }
}
