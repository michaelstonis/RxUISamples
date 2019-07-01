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

        [Reactive] public ReactiveCommand<Unit, Unit> AttemptLogin { get; private set; }

        public override void RegisterObservables(CompositeDisposable viewModelDisposables)
        {
            AttemptLogin =
                ReactiveCommand
                    .CreateFromTask(
                        async () =>
                        {
                            this.Log().Info<LoginViewModel>("Starting Login");
                            await Task.Delay(TimeSpan.FromMilliseconds(5000));
                            this.Log().Info<LoginViewModel>("Finished Login");
                        })
                    .DisposeWith(viewModelDisposables);
        }
    }
}
