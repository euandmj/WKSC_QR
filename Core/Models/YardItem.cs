using Newtonsoft.Json;
using System;

namespace Core.Models
{
    [JsonObject]
    public sealed class YardItem
        : IEquatable<YardItem>
    {
        [JsonIgnore]
        private char _zone;
        [JsonIgnore]
        private short _zoneNum;

        public YardItem()
            : this(Guid.NewGuid()) { }
        
        public YardItem(Guid id)
        {
            Id = id;
        }

        [JsonIgnore]
        public Guid Id { get; }
        [JsonProperty]
        public string Owner { get; set; }
        [JsonProperty]
        public BoatClass BoatClass { get; set; }
        [JsonProperty]
        public DateTime DueDate { get; set; }
        [JsonProperty] 
        public bool Starred { get; set; }


        [JsonIgnore] 
        public bool HasPaid
            => DueDate > DateTime.Now;

        [JsonIgnore]
        public string ZoneBoat
            => $"{Zone} - {BoatClass}";

        [JsonProperty] 
        public string Zone
        {
            get => $"{_zone}{_zoneNum}".ToUpper();
            set
            {
                // parse zoneChar and zoneShort from value
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                if (value.Length < 2) throw new ArgumentException("zone setter expects {char}{number}", nameof(value));

                var @char = value[0];
                var numStr = value.Substring(1);

                if (!short.TryParse(numStr, out short num))
                    throw new ArgumentException($"zone setter expects short parsable number. Got - {numStr}", nameof(value));

                _zone = @char;
                _zoneNum = num;
            }
        }

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
    }

    public enum BoatClass
        : int
    {
        Unknown = 0,
        Falcon,
        Laser,
        GP14
    }
}
