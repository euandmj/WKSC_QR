using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Core
{
    public class YardItemSerializer
    {

        public static string Serialize(ICollection<YardItem> items)
        {
            return JsonConvert.SerializeObject(items);
        }

        public static ICollection<YardItem> Deserialize(string json)
        {
            if (json is null)
                throw new ArgumentNullException(nameof(json));

            return JsonConvert.DeserializeObject<ICollection<YardItem>>(json);
        }




    }
}
