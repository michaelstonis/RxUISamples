using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using System.Reactive.Disposables;

namespace RxUISamples.UserInterface.Pages
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            this.ViewModel = new ViewModels.LoginViewModel();

            InitializeComponent();

            this.WhenActivated(
                (CompositeDisposable viewDisposables) =>
                {
                });
        }
    }
}
