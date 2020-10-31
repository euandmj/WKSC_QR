using System;

namespace Core.Models
{
    public sealed class YardItem
    {
        private char _zone;
        private short _zoneNum;

        public YardItem(char c, short num)
            : this(Guid.NewGuid(), c, num) { }

        public YardItem(Guid id, char c, short num)
            => (Id, _zone, _zoneNum) = (id, c, num);
        
        public YardItem(Guid id)
        {
            Id = id;
        }


        public Guid Id { get; }
        public string Owner { get; set; }
        public BoatClass BoatClass { get; set; }
        public DateTime DueDate { get; set; }

        public bool HasPaid
            => DueDate > DateTime.Now;

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

                if (short.TryParse(numStr, out short num))
                    throw new ArgumentException($"zone setter expects short parsable number. Got - {numStr}", nameof(value));

                _zone = @char;
                _zoneNum = num;
            }
        }

        public override int GetHashCode() => this.Id.GetHashCode();
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
