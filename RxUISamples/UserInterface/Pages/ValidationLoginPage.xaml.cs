using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using System.Reactive.Disposables;
using ReactiveUI.Validation.Extensions;
using System.Reactive.Linq;

namespace RxUISamples.UserInterface.Pages
{
    public partial class ValidationLoginPage
    {
        public ValidationLoginPage()
        {
            this.ViewModel = new ViewModels.ValidationLoginViewModel();

            InitializeComponent();

            this.WhenActivated(
                (CompositeDisposable viewDisposables) =>
                {
                    this.Bind(ViewModel, vm => vm.Username, view => view.username.Text)
                        .DisposeWith(viewDisposables);

                    this.Bind(ViewModel, vm => vm.Password, view => view.password.Text)
                        .DisposeWith(viewDisposables);

                    this.BindCommand(ViewModel, vm => vm.AttemptLogin, view => view.login)
                        .DisposeWith(viewDisposables);

                    this.BindValidation(ViewModel, vm => vm.UsernameValidation, view => view.usernameValidation.Text)
                        .DisposeWith(viewDisposables);

                    this.WhenAnyValue(x => x.ViewModel.UsernameValidation.IsValid)
                        .Select(isValid => 
                            isValid ? 
                            Color.Green : Color.Red)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .BindTo(this, view => view.username.BackgroundColor)
                        .DisposeWith(viewDisposables);

                    this.BindValidation(ViewModel, vm => vm.PasswordValidation, view => view.passwordValidation.Text)
                        .DisposeWith(viewDisposables);

                    this.WhenAnyValue(x => x.ViewModel.PasswordValidation.IsValid)
                        .Select(isValid => isValid ? (Color)Label.TextColorProperty.DefaultValue : Color.Red)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .BindTo(this, view => view.password.BackgroundColor)
                        .DisposeWith(viewDisposables);
                });
        }
    }
}
