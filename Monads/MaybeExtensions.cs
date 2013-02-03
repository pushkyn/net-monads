using System;

namespace Monads
{
    public static class MaybeExtensions
    {
        public static Maybe<T> ToMaybe<T>(this T input)
        {
            return new Maybe<T>(input);
        }

        public static Maybe<TResult> With<TInput, TResult>(this Maybe<TInput> maybe, Func<TInput, TResult> func)
        {
            return maybe.HasValue ? new Maybe<TResult>(func(maybe.Value)) : Maybe<TResult>.None;
        }

        public static TResult Return<TInput, TResult>(this Maybe<TInput> maybe, Func<TInput, TResult> func, TResult defaultValue)
        {
            var result = defaultValue;
            if (maybe.HasValue)
                result = func(maybe.Value);
            return result;
        }

        public static Maybe<TC> SelectMany<TA, TB, TC>(this Maybe<TA> a, Func<TA, TB> func, Func<TA, TB, TC> select)
        {
            return a.With(aval => func(aval)
                                      .ToMaybe().With(bval => select(aval, bval)).Value);
        }

        public static Maybe<TB> Select<TA, TB>(this Maybe<TA> a, Func<TA, TB> func)
        {
            return a.With(func);
        }
    }
}
