﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Window
    {
        const string HELP_Import = @"\help\Help_Import.docx";
        const string HELP_Export = @"\help\Help_Export.docx";     

        public HelpPage()
        {
            InitializeComponent();
        }

        private void OpenHelp(string which)
        {
            string dir = string.Empty;
            try
            {
                
                dir = Assembly.GetExecutingAssembly().Location + which;
                Process.Start(dir);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error opening help file " + dir, ex.Message);
            }
        }

        private void HowToImport_Click(object sender, RoutedEventArgs e)
        {
            OpenHelp(HELP_Import);
        }

        private void HowToSelect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HowToExport_Click(object sender, RoutedEventArgs e)
        {
            OpenHelp(HELP_Export);
        }
    }
}