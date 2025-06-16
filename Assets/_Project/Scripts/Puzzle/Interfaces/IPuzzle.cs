using UnityEngine;

namespace AE
{
    public interface IPuzzle
    {
        bool IsSolved { get; }
        void CheckSolution();
        void OnPuzzleSolved();
    }
}
