using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DependencyInjection;
using Core.Models;
using Data.Providers;
using Data.Services;
using System.Threading.Tasks;

namespace GUI.Providers
{
    class RecentlyEditedYardItemProvider
        : YardItemProviderBase
    {
        private readonly IDataStore<YardItem, string> _dataStore;
        private readonly int _recentNumber;

        public RecentlyEditedYardItemProvider(int numberRecent)
        {
            _recentNumber = numberRecent;
            _dataStore = ContainerClass.ResolveType<IDataStore<YardItem, string>>();
        }

        public override YardItem GetItem(string x)
        {
            return _dataStore.GetItem(x);
        }

        public override IEnumerable<YardItem> GetItems()
        {
            return _dataStore
                        .GetItems()
                        .Take(_recentNumber)
                        .OrderByDescending(x => x.LastUpdated);
        }

        public override IEnumerable<YardItem> GetItems(Func<YardItem, bool> filter)
        {
            throw new NotImplementedException();
        }
    }
}
