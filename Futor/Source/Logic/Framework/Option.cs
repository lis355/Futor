using System;

namespace Futor
{
    public class Option
    {
    }

    public class Option<T> : Option
    {
        readonly Func<T> _getter;
        readonly Action<T> _setter;

        public class ChangeEventArgs : EventArgs
        {
            public T OldValue;
            public T NewValue;
        }

        public event EventHandler<ChangeEventArgs> OnChanged;

        public Option(Func<T> getter, Action<T> setter, EventHandler<ChangeEventArgs> onChanged = null)
        {
            _getter = getter;
            _setter = setter;

            if (onChanged != null)
                OnChanged += onChanged;
        }

        public T Value
        {
            get => Get();
            set
            {
                value = Validate(value);

                if (_setter == null
                    || Equals(Value, value))
                    return;

                var oldValue = Value;

                Set(value);

                OnChanged?.Invoke(this, new ChangeEventArgs {OldValue = oldValue, NewValue = Value});
            }
        }

        public virtual T Validate(T value)
        {
            return value;
        }

        public virtual T Get()
        {
            return (_getter != null) ? _getter() : default(T);
        }

        public virtual void Set(T value)
        {
            _setter(value);
        }
    }

    public class OptionMinMax<T> : Option<T> where T : IComparable<T>
    {
        public T Min { get; set; }
        public T Max { get; set; }

        public OptionMinMax(Func<T> getter, Action<T> setter, T min, T max, EventHandler<ChangeEventArgs> onChanged = null) :
            base(getter, setter, onChanged)
        {
            if (min.CompareTo(max) > 0)
                throw new ArgumentNullException(nameof(min));

            Min = min;
            Max = max;
        }

        public override T Validate(T value)
        {
            if (value.CompareTo(Min) < 0)
                value = Min;

            if (value.CompareTo(Max) > 0)
                value = Max;

            return value;
        }
    }
}
