using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.View.CellPicker
{
    enum EventType
    {
        Unchecked = 0,
        Checked
    }

    class CellCheckedEventArgs
        : EventArgs
    {

        public ClickableCell Cell { get; }
        public EventType Type { get; }



        public CellCheckedEventArgs(ClickableCell cell, EventType et)
        {
            Cell = cell;
            Type = et;
        }
    }
}
