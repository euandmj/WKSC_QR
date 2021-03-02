using Data.CSV;
using System.Collections.Generic;

namespace Data.Models
{
    public class YardItemColumnSchema
        : IColumnSchema
    {


        public YardItemColumnSchema(params (string, int)[] columns)
        {
            Columns = new Dictionary<string, int>(columns.Length);

            foreach(var (col, index) in columns)
            {
                Columns[col] = index;
            }
        }

        public int this[string index] => Columns[index];

        public IDictionary<string, int> Columns { get; set; }

        public int Count => Columns.Count;

        public bool Equals(IColumnSchema other)
        {
            //if (Columns.Length < other.Columns.Length) return false;

            //for (int i = 0; i < other.Columns.Length; i++)
            //{
            //    if (other.Columns[i].columnName != Columns[i].columnName)
            //        return false;
            //}
            return true;
        }
    }
}
