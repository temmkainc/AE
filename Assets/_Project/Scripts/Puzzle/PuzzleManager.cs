using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    public class PuzzleManager : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> puzzleScripts;

        private List<IPuzzle> puzzles = new List<IPuzzle>();
        private bool allPuzzlesSolved = false;

        [SerializeField] private Transform finalWall;
        [SerializeField] private float secretWallMoveDistance = 3f;
        [SerializeField] private float secretWallMoveDuration = 2f;

        private void Start()
        {
            foreach (var script in puzzleScripts)
            {
                if (script is IPuzzle puzzle)
                {
                    puzzles.Add(puzzle);
                }
            }
        }

        private void FixedUpdate()
        {
            if (allPuzzlesSolved) return;

            foreach (var puzzle in puzzles)
            {
                if (!puzzle.IsSolved)
                    return;
            }

            allPuzzlesSolved = true;
            OnAllPuzzlesSolved();
        }

        private void OnAllPuzzlesSolved()
        {
            Debug.Log("All puzzles solved!");

            if (finalWall != null)
            {
                Vector3 targetPos = finalWall.position + Vector3.down * secretWallMoveDistance;
                finalWall.DOMove(targetPos, secretWallMoveDuration).SetEase(Ease.InOutSine);
            }

        }
    }
}
