using UnityEngine;

namespace _MiniGame
{
    public interface IMovable
    {
        bool IsInitialize { get; }
        Vector3 Position { get; }
        void SetDestination(Vector3 direction);
    }
}