using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace RxUISamples.UserInterface.Pages
{
    public partial class InteractionPage
    {
        public InteractionPage()
        {
            this.ViewModel = new ViewModels.InteractionViewModel();
            InitializeComponent();


            this.WhenActivated(
                (CompositeDisposable viewDisposables) =>
                {
                    this.OneWayBind(ViewModel, vm => vm.WorkStatus, view => view.workStatus.Text)
                        .DisposeWith(viewDisposables);

                    this.BindCommand(ViewModel, vm => vm.StartWorking, view => view.startWork)
                        .DisposeWith(viewDisposables);

                    this.ViewModel
                        .WorkflowConfirmation
                        .RegisterHandler(
                            async x =>
                            {
                                var result = await DisplayAlert("Work Status", x.Input, "yes", "no");

                                x.SetOutput(result);
                            })
                        .DisposeWith(viewDisposables);

                });
        }
    }
}
