using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RxUISamples.Extensions
{
    public static class DisposableExtensions
    {
        public static IObservable<Unit> NotifyOfDisposal(this CompositeDisposable disposable)
        {
            var disposed = new Subject<Unit>();

            Disposable
                .Create(
                    () =>
                    {
                        //disposed.OnNext(Unit.Default);
                        //disposed.OnCompleted();
                        //disposed.Dispose();
                        //disposed = null;
                    })
                .DisposeWith(disposable);

            return disposed.AsObservable();
        }
    }
}
