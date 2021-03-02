using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Data.CSV
{
    public interface IColumnSchema 
        : IEquatable<IColumnSchema>
    {
        [JsonProperty]
        IDictionary<string, int> Columns { get; set; }

        [JsonIgnore]
        int Count { get; }

        int this[string index]
        {
            get;
        }
    }
}
