using Core.Models;
using Data.CSV;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Models
{
    public class YardItemCSVSchema
        : ICSVSchema<YardItem>
    {
        public const string ZONE_Col      = "ROW";
        public const string CLASS_Col     = "CLASS";
        public const string SAIL_Col      = "SAIL";
        public const string OWNER_Col     = "OWNER";

        public YardItemCSVSchema(IColumnSchema cols)
        {
            ColumnSchema = cols;
        }

        public Type AssociatedType => typeof(YardItem);

        public IColumnSchema ColumnSchema { get; private set; }

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
                var objs = new List<object>(ColumnSchema.Columns.Count);
                foreach (var col in ColumnSchema.Columns)
                {
                    objs.Add(ParseLine<string>(cells[col.Value]));
                }

                return objs.ToArray();             
            }
            catch (Exception ex) { return null; }
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
                    Zone        = dr[ZONE_Col].ToString(),
                    BoatClass   = dr[CLASS_Col].ToString(),
                    SailNumber  = dr[SAIL_Col].ToString(),
                    Owner       = dr[OWNER_Col].ToString()
                };
            }
            catch (Exception ex) { return null; }
        }

        public IEnumerable<YardItem> ParseTable(DataTable dt)
        {
            foreach(DataRow row in dt.Rows)
            {
                var yardItem = ParseRow(row);

                if (yardItem != null) yield return yardItem;
            }
        }
    }
}