﻿using GUI.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GUI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            try
            {
                //await Shell.Current.GoToAsync($"//{nameof(ScanPage)}");
                await Shell.Current.Navigation.PushModalAsync(new ScanPage());

            }
            catch (Exception ex)
            {

            }
        }
    }
}
