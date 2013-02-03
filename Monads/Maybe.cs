namespace Monads
{
    public sealed class Maybe<T>
    {
        public static Maybe<T> None = new Maybe<T>(default(T));

        public T Value { get; set; }

        public bool HasValue
        {
            get
            {
                var hasValue = false;
                if (Value != null && !Value.Equals(default(T)))
                    hasValue = true;
                return hasValue;
            }
        }

        public Maybe(T value)
        {
            Value = value;
        }
    }
}
