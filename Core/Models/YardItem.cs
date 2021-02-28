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

        //public string One { get; set; }
        //public string Two { get; set; }

        [JsonIgnore]
        public Guid Id { get; }
        [JsonProperty]
        public string Owner { get; set; }
        [JsonProperty]
        public string BoatClass { get; set; }
        [JsonProperty]
        public DateTime DueDate { get; set; } = DateTime.MinValue;
        [JsonProperty]
        public short SailNumber { get; set; }
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
                try
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
                catch(ArgumentException) // catch proper except
                {
                    if(short.TryParse(value, out short num)){
                        _zoneNum = num;
                    }
                }
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
