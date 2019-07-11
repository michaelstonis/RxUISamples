using System;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using RxUISamples.Extensions;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RxUISamples.ViewModels
{
    public class CancelProcessingViewModel : ViewModelBase
    {
        private readonly TimeSpan _processingDelay = TimeSpan.FromSeconds(.25);

        [Reactive] public string ProcessingMessage { get; set; }

        [Reactive] public ReactiveCommand<Unit, Unit> StartProcessing { get; private set; }
        [Reactive] public ReactiveCommand<Unit, Unit> CancelProcessing { get; private set; }

        public override void RegisterObservables(CompositeDisposable viewModelDisposables)
        {
            StartProcessing =
                ReactiveCommand
                    .CreateFromObservable(
                        () =>
                            Observable
                                .FromAsync (
                                    async ct => 
                                    {
                                        var timer = new Stopwatch();

                                        timer.Start();

                                        ProcessingMessage = $"Processing started";

                                        while (!ct.IsCancellationRequested && !(viewModelDisposables?.IsDisposed ?? true))
                                        {
                                            ProcessingMessage = $"Processing for {timer.ElapsedMilliseconds}ms";
                                            await Task.Delay(_processingDelay);
                                        }

                                        timer.Stop();

                                        ProcessingMessage = $"Processing stopped at {timer.ElapsedMilliseconds}ms";
                                    })
                                .TakeUntil(this.WhenAnyObservable(x => x.CancelProcessing)),
                        this.WhenAnyObservable(x => x.StartProcessing.IsExecuting).IsNot().StartWith(false))
                    .DisposeWith(viewModelDisposables);
                    
            CancelProcessing = 
                ReactiveCommand
                    .Create(
                        () => { },
                        this.WhenAnyObservable(x => x.StartProcessing.IsExecuting).StartWith(false))
                    .DisposeWith(viewModelDisposables);
        }
    }
}
