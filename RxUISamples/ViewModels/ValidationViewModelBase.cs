using System;
using System.Reactive.Disposables;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;

namespace RxUISamples.ViewModels
{
    public abstract class ValidationViewModelBase : ViewModelBase, ISupportsValidation
    {
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public abstract void RegisterValidation(CompositeDisposable viewModelDisposables);
    }
}
