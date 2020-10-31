using Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Data
{
    public class AdapterCSV
    {





        //public static async Task<IEnumerable<YardItem>> ReadCSV(string path, char delimiter = ',')
        //{
        //    var items = new HashSet<YardItem>();

        //    using (var reader = new StreamReader(path))
        //    {
        //        while (!reader.EndOfStream)
        //        {
        //            var line = await reader.ReadLineAsync();
        //            var cells = line.Split(delimiter);

        //            yield return(new YardItem(Guid.Parse(cells[0]))
        //            {
        //                Owner = cells[1],
        //                Zone = cells[2],
        //                BoatClass = (BoatClass)Enum.Parse(typeof(BoatClass), cells[3], true),
        //                DueDate = DateTime.Parse(cells[4])
        //            });
        //        }
        //    }
        //}
    }
}
