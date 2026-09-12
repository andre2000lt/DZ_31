using System;

namespace _MiniGame
{
    public interface IReadOnlyReactVariable<T>
    {
        event Action<T, T, T> Changed;

        T Value { get; }
    }
}