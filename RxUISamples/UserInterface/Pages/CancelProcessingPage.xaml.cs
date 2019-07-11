using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive;

namespace RxUISamples.UserInterface.Pages
{
    public partial class CancelProcessingPage
    {
        public CancelProcessingPage()
        {
            this.ViewModel = new ViewModels.CancelProcessingViewModel();
            InitializeComponent();


            this.WhenActivated(
                (CompositeDisposable viewDisposables) =>
                {
                    this.OneWayBind(ViewModel, vm => vm.ProcessingMessage, view => view.processingMessage.Text)
                        .DisposeWith(viewDisposables);

                    this.BindCommand(ViewModel, vm => vm.StartProcessing, view => view.startProcessing)
                        .DisposeWith(viewDisposables);

                    this.BindCommand(ViewModel, vm => vm.CancelProcessing, view => view.cancelProcessing)
                        .DisposeWith(viewDisposables);

                    this.WhenAnyValue(x => x.processingMessage)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .BindTo(this, view => view.processingMessage.Text)
                        .DisposeWith(viewDisposables);

                    this.WhenAnyObservable(x => x.ViewModel.CancelProcessing)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .SelectMany(
                            async _ =>
                            {
                                await this.DisplayAlert("Cancelled", "Processing Cancelled", "OK");
                                return Unit.Default;
                            })
                        .Subscribe()
                        .DisposeWith(viewDisposables);
                });
        }
    }
}
