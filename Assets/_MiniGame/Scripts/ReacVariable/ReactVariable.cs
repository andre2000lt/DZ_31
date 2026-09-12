using System;

namespace _MiniGame
{
    public class ReactVariable<T> : IReadOnlyReactVariable<T> where T : IEquatable<T>
    {
        public event Action<T, T, T> Changed;

        public ReactVariable(T value)
        {
            _value = value;
            _maxValue = _value;
        }

        private T _value;
        private T _maxValue;
        public T Value
        {
            get => _value;
            set
            {
                if (value.Equals(_value)) return;

                T oldValue = _value;
                _value = value;

                Changed?.Invoke(oldValue, value, _maxValue);
            }
        }
    }
}