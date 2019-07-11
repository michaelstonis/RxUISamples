using System;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace RxUISamples.ViewModels
{
    public class BetterViewModel : ReactiveObject
    {
        [Reactive] public string FirstName { get; set; }
        [Reactive] public string LastName { get; set; }

        public string FormattedName { [ObservableAsProperty] get; }

        public BetterViewModel()
        {
            this.WhenAnyValue(
                vm => vm.FirstName,
                vm => vm.LastName,
                (first, last) => $"{last}, {first}")
            .ToPropertyEx(this, x => x.FormattedName);
        }
    }
}
