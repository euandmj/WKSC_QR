using Core.Models;
using System;
using System.Collections.Generic;

namespace Data.Providers
{
    public abstract class YardItemProviderBase
        : IProvider<YardItem, string>
    {       


        public abstract YardItem GetItem(string x);
        public abstract IEnumerable<YardItem> GetItems();
        public abstract IEnumerable<YardItem> GetItems(Func<YardItem, bool> filter);
    }
}
