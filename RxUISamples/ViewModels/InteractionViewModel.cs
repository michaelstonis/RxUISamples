using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace RxUISamples.ViewModels
{
    public class InteractionViewModel : ViewModelBase
    {
        public Interaction<string, bool> WorkflowConfirmation { get; }
            = new Interaction<string, bool>();

        [Reactive] public string WorkStatus { get; private set; }

        [Reactive] public ReactiveCommand<Unit,Unit> StartWorking { get; private set; }

        public override void RegisterObservables(CompositeDisposable viewModelDisposables)
        {
            StartWorking =
ReactiveCommand
    .CreateFromTask(
        async () => 
        {
            var doesWantToWork = await WorkflowConfirmation.Handle("Are you sure you want to work?");

            if(!doesWantToWork)
            {
                WorkStatus = "Phew! We thought we were going to actually have to do something";
                return;
            }

            var reallyWantsToWork = await WorkflowConfirmation.Handle("Work is hard though, are you REALLY sure?");

            WorkStatus =
                reallyWantsToWork
                    ? "I guess we can work, but I'm not happy about it"
                    : "Please don't scare us next time and just ";
        })
                    .DisposeWith(viewModelDisposables);
        }
    }
}
