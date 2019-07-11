using System;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using RxUISamples.Extensions;
using System.Threading.Tasks;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;

namespace RxUISamples.ViewModels
{
    public class ValidationLoginViewModel : ValidationViewModelBase
    {
        [Reactive] public string Username { get; set; }
        [Reactive] public string Password { get; set; }

        [Reactive] public ValidationHelper PasswordValidation { get; private set; }
        [Reactive] public ValidationHelper UsernameValidation { get; private set; }

        [Reactive] public ReactiveCommand<Unit, Unit> AttemptLogin { get; private set; }

        public override void RegisterObservables(CompositeDisposable viewModelDisposables)
        {
            AttemptLogin =
                ReactiveCommand
                    .CreateFromTask(
                        async () =>
                        {
                            await Task.Delay(TimeSpan.FromMilliseconds(5000));
                        },
                        this.WhenAnyObservable(x => x.ValidationContext.Valid).StartWith(false))
                    .DisposeWith(viewModelDisposables);
        }

        public override void RegisterValidation(CompositeDisposable viewModelDisposables)
        {
            UsernameValidation =
this.ValidationRule(
    vm => vm.Username,
    username => !string.IsNullOrEmpty(username),
    "You must provide a username")
    .DisposeWith(viewModelDisposables);

            PasswordValidation =
                this.ValidationRule(
                    vm => vm.Password,
                    username => !string.IsNullOrEmpty(username),
                    "You must provide a password")
                    .DisposeWith(viewModelDisposables);
        }
    }
}
