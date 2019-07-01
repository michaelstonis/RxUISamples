using System;
using System.Reactive.Linq;
namespace RxUISamples.Extensions
{
    public static class ObservableExtensions
    {
        public static IObservable<bool> IsNot(this IObservable<bool> observable)
        {
            return observable.Select(x => !x);
        }
    }
}
