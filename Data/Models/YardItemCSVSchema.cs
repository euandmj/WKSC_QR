using Core.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Models
{
    public class YardItemCSVSchema
        : ICSVSchema<YardItem>
    {
        public (string columnName, Type type)[] Columns
            => new (string columnName, Type type)[]
        {
            ("name", typeof(string)),
            ("zone", typeof(string)),
            ("boat", typeof(BoatClass)),
            ("due", typeof(DateTime))
        };

        public Type AssociatedType => typeof(YardItem);           

        public bool Equals(string[] splitHeaders)
        {
            for (int i = 0; i < splitHeaders.Length; i++)
            {
                if (splitHeaders[i] != Columns[i].columnName)
                    return false;
            }
            return true;
        }

        private T ParseLine<T>(object val, Func<object, T> convertFunc = null)
        {
            try
            {
                return convertFunc == null
                    ? (T)val
                    : convertFunc(val);
            }
            catch(Exception ex) 
            when (ex is ArgumentException || ex is InvalidCastException || ex is FormatException) 
            { 
                return default; 
            }
        }

        public object[] ParseLines(string[] cells)
        {
            try
            {
                return new object[]
                {
                    ParseLine<string>(cells[0]),
                    ParseLine<string>(cells[1]),
                    ParseLine(cells[2], (x) => (BoatClass)Enum.Parse(typeof(BoatClass), cells[2])),
                    ParseLine(cells[2], (x) => DateTime.Parse(cells[3]))
                };
            }
            catch(IndexOutOfRangeException)
            {
                throw;
            }
            catch(FormatException)
            {
                throw;
            }
        }

        public YardItem ParseRow(DataRow dr)
        {
            try
            {
                return new YardItem()
                {
                    Owner = (string)dr["name"],
                    Zone = (string)dr["zone"],
                    BoatClass = (BoatClass)dr["boat"],
                    DueDate = (DateTime)dr["due"]
                };
            }
            catch(InvalidCastException)
            {
                throw;
            }
        }

        public IEnumerable<YardItem> ParseTable(DataTable dt)
        {
            foreach(DataRow row in dt.Rows)
            {
                yield return ParseRow(row);
            }
        }
    }
}