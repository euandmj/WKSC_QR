using Core.DependencyInjection;
using Core.Models;
using Data.Providers;
using Data.Services;
using System;
using System.Collections.Generic;

namespace GUI.Providers
{
    class YardItemProvider
        : YardItemProviderBase
    {
        readonly IDataStore<YardItem, string> _dataStore;

        public YardItemProvider(
            Action<IEnumerable<YardItem>> onNewData)
        {
            _dataStore = AutofacResolver.ResolveType<IDataStore<YardItem, string>>();

            _dataStore.Refreshed += (e, func) =>
            {
                onNewData(func());
            };
        }


        public override YardItem GetItem(string x)
        {
            return _dataStore.GetItem(x);
        }

        public override IEnumerable<YardItem> GetItems()
        {
            return _dataStore.GetItems();
        }

        public override IEnumerable<YardItem> GetItems(Func<YardItem, bool> filter)
        {
            return _dataStore.GetItems(filter);
        }
    }
}
