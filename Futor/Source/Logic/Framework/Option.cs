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
            get => (_getter != null) ? _getter() : default(T);
            set
            {
                if (_setter == null
                    || Equals(Value, value))
                    return;

                var oldValue = Value;

                _setter(value);

                OnChanged?.Invoke(this, new ChangeEventArgs {OldValue = oldValue, NewValue = Value});
            }
        }
    }
}
