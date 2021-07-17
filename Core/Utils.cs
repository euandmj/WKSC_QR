using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Core
{
    public static class Utils
    {

        public static void Set4Ground()
        { 
            /*
            string title,
            Action<string> SetTitle)
        {
            var originalTitle = title;
            var uniqueTitle = $"{new Guid()}";
            SetTitle(uniqueTitle);
            var handle = Externs.FindWindowByCaption(IntPtr.Zero, uniqueTitle);
            */
            var handle = Externs.GetConsoleWindow();
            if (handle == IntPtr.Zero)
            {
                Debug.WriteLine("Failed to find handle for captioned process- {uniqueTitle}");
                return;
            }

            Externs.SetForegroundWindow(handle);

            //SetTitle(originalTitle);
        }


        private static class Externs
        {
            [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
            public static extern IntPtr FindWindowByCaption(IntPtr zeroOnly, string lpWindowName);

            [DllImport("USER32.dll")]
            public static extern bool SetForegroundWindow(IntPtr ptr);

            [DllImport("kernel32.dll", ExactSpelling = true)]
            public static extern IntPtr GetConsoleWindow();
        }
    }
}
