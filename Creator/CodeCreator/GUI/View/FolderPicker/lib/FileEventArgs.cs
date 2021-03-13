using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.View.FolderPicker
{
    public class FileEventArgs
        : EventArgs
    {
        public FileEventType @Type { get; }


        public FileEventArgs(FileEventType @type)
        {
            Type = type;
        }




    }

    public delegate void FileEventHandler(FileEventArgs e);


    public enum FileEventType
    {
        created,
        deleted,
        renamed,
        changed
    }
}
