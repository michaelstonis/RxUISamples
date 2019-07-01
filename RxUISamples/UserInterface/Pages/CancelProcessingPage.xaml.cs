using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using System.Reactive.Linq;

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
                });
        }
    }
}
