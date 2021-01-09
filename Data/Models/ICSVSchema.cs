using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Models
{
    public interface ICSVSchema<T>
    {
        Type AssociatedType { get; }
        (string columnName, Type type)[] Columns { get; }

        bool Equals(string[] splitHeaders);
        T ParseRow(DataRow dr);
        IEnumerable<T> ParseTable(DataTable dr);
        object[] ParseLines(string[] items);
    }
}
