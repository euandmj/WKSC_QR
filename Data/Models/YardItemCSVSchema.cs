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

        private bool TryGetFlagged(DataRow dr)
        {
            try
            {
                return dr["FLAGGED"].ToString().Length > 0;
            }
            catch { return false; }
        }

        public object[] ParseCells(string[] cells)
        {
            try
            {
                var objs = new List<object>(ColumnSchema.Columns.Count);
                foreach (var col in ColumnSchema.Columns)
                {
                    // schema has specified a column that does not exist within the csv
                    if (col.Value - 1 > cells.Length) continue;

                    objs.Add(ParseLine<string>(cells[col.Value]));
                }

                return objs.ToArray();             
            }
            catch (Exception ex) 
            { 
                return null; 
            }
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
                    Owner       = dr[OWNER_Col].ToString(),
                    Flagged     = TryGetFlagged(dr) 
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