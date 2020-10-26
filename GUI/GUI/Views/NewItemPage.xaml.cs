using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GUI.Models;
using GUI.ViewModels;

namespace GUI.Views
{
    public partial class NewItemPage : ContentPage
    {
        public YardItem Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}