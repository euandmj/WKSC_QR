using Core.Encoding;
using Core.Models;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GUI.Export
{
    static class ExportYardItemUtility
    {
        public static CommonFileDialogResult TryGetFolder(out string path)
        {
            using (var dia = new CommonOpenFileDialog())
            {
                dia.IsFolderPicker = true;
                dia.EnsurePathExists = true;
                var res = dia.ShowDialog();

                path = dia.FileName;

                return res;
            }
        }

        public static ISet<string> SaveToFile(
            string directory,
            ICollection<YardItem> items,
            IQREncoder<YardItem> encoder)
        {
            var created = new HashSet<string>(items.Count);

            foreach (var item in items)
            {
                try
                {
                    using (var bmp = encoder.Encode(item))
                    {
                        var path = Path.Combine(directory, $"{DateTime.Now:dd-MM-yyyy} - {item.Owner}.bmp");

                        bmp.Save(path);
                        created.Add(path);
                    }
                }
                catch (IOException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("failed exporting bitmaps " + ex.Message);
                }
            }
            return created;
        }

        public static void Print(
            IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                try
                {
                    ProcessStartInfo info = new ProcessStartInfo
                    {
                        Verb = "print",
                        FileName = path,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };

                    new Process
                    {
                        StartInfo = info
                    }.Start();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("failed print bitmaps " + ex.Message);
                }
            }
        }
    }
}
