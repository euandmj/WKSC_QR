using Newtonsoft.Json;
using System;

namespace Core.Models
{
    [JsonObject]
    public sealed class YardItem
        : IEquatable<YardItem>
    {
        public YardItem()
            : this(Guid.NewGuid()) { }
        
        public YardItem(Guid id)
        {
            Id = id;
        }

        [JsonProperty]
        public Guid Id { get; }
        [JsonProperty]
        public string Owner { get; set; }
        [JsonProperty]
        public string BoatClass { get; set; }
        [JsonProperty]
        public DateTime DueDate { get; set; } = DateTime.MinValue;
        [JsonProperty]
        public string SailNumber { get; set; }
        [JsonIgnore] 
        public bool Starred { get; set; }
        [JsonProperty]
        public string Zone { get; set; }


        [JsonIgnore] 
        public bool HasPaid
            => DueDate > DateTime.Now;

        [JsonIgnore]
        public string ZoneBoat
            => $"{Zone} - {BoatClass}";


        public override int GetHashCode() => this.Zone.GetHashCode();

        public override bool Equals(object obj)
        {
            return 
                obj is YardItem other 
                ? Equals(other)
                : GetHashCode() == obj.GetHashCode();
        }

        public bool Equals(YardItem other)
        {
            return this.Zone == other.Zone;
        }

        public static YardItem Invalid
        {
            get
            {
                return new YardItem(Guid.Empty)
                {
                    Owner = "INVALID_YARD_ITEM"
                };
            }            
        }
    }

    public enum BoatClass
        : int
    {
        Unknown = 0,
        Falcon,
        Laser,
        GP14,
        Oppy

            
    }
}
