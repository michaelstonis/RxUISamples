using System;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using RxUISamples.Extensions;
using System.Threading.Tasks;
using Splat;

namespace RxUISamples.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        [Reactive] public string Username { get; set; }
        [Reactive] public string Password { get; set; }

        public bool IsLoggingIn { [ObservableAsProperty] get; }

        [Reactive] public ReactiveCommand<Unit, bool> AttemptLogin { get; private set; }

        public override void RegisterObservables(CompositeDisposable viewModelDisposables)
        {
            AttemptLogin =
                ReactiveCommand
                    .CreateFromTask(
                        async () =>
                        {
                            this.Log().Info("Starting Login");
                            var result = await ProcessLogin();
                            this.Log().Info($"Finished Login");

                            return result;
                        },
                        this.WhenAnyValue(x => x.Username, x => x.Password,
                            (username, password) =>
                                !string.IsNullOrWhiteSpace(username) &&
                                !string.IsNullOrWhiteSpace(password)))
                    .DisposeWith(viewModelDisposables);

            this.WhenAnyObservable(x => x.AttemptLogin.IsExecuting)
                .StartWith(false)
                .ToPropertyEx(this, x => x.IsLoggingIn)
                .DisposeWith(viewModelDisposables);
        }

        private async Task<bool> ProcessLogin()
        { 
            await Task.Delay(TimeSpan.FromMilliseconds(5000));
            return DateTime.Now.Second % 2 == 0;
        }
    }
}
