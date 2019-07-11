using System;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using ReactiveUI;
namespace RxUISamples.ViewModels
{
    public class SimpleViewModel : ReactiveObject
    {
        string _firstName;

        [DataMember]
        public string FirstName
        {
            get => _firstName;
            set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }

        string _lastName;

        [DataMember]
        public string LastName
        {
            get => _lastName;
            set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }

        ObservableAsPropertyHelper<string> _formattedName;

        public string FormattedName => _formattedName?.Value ?? default(string);

        public SimpleViewModel()
        {
            this.WhenAnyValue(
                    vm => vm.FirstName,
                    vm => vm.LastName,
                    (first, last) => $"{last}, {first}")
                .ToProperty(this, nameof(FormattedName));

            this.WhenAnyValue(vm => vm.FormattedName)
                .Subscribe(x => Console.WriteLine(x));
        }
    }
}
