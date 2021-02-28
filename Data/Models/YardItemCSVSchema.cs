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
            ("ROW", typeof(string)),
            ("CLASS", typeof(string)),
            ("OWNER", typeof(string))
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
            // catch conversion on each line
            try
            {
                return new object[]
                {
                    //ParseLine<string>(cells[0]),
                    //ParseLine<string>(cells[1]),
                    //ParseLine(cells[2], (x) => (BoatClass)Enum.Parse(typeof(BoatClass), cells[2])),
                    //ParseLine(cells[2], (x) => DateTime.Parse(cells[3]))
                    ParseLine<string>(cells[0]),
                    ParseLine<string>(cells[1]),
                    ParseLine<string>(cells[5])
                };
            }
            catch (Exception ex) { return new object[] { 1, 2, 3, 4 }; }
            //catch(IndexOutOfRangeException)
            //{
            //    throw;
            //}
            //catch(FormatException)
            //{
            //    throw;
            //}
        }

        public YardItem ParseRow(DataRow dr)
        {
            try
            {
                return new YardItem()
                {
                    //Owner = (string)dr["name"],
                    Zone = dr[0].ToString(),
                    BoatClass = dr[1].ToString(),
                    Owner = dr[2].ToString()
                    
                };
            }
            catch (Exception ex) { return YardItem.Invalid; }
            //catch (InvalidCastException)
            //{
            //    throw;
            //}
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