using System;
using System.Reactive.Disposables;
using ReactiveUI;
namespace RxUISamples.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, ISupportsActivation
    {
        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        public ViewModelBase()
        {
            this.WhenActivated(
                (CompositeDisposable viewModelDisposables) =>
                {
                    RegisterObservables(viewModelDisposables);

                    if(this is ValidationViewModelBase vvmb)
                    {
                        vvmb.RegisterValidation(viewModelDisposables);
                    }
                });
        }

        public abstract void RegisterObservables(CompositeDisposable viewModelDisposables);
    }
}
