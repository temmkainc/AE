using DG.Tweening;
using System.Buffers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AE
{
    public class SkullsPuzzle : MonoBehaviour, IPuzzle
    {
        [SerializeField] private List<RotatableSkull> rotatableSkulls;

        [Header("Secret Wall")]
        [SerializeField] private Transform secretWall;
        [SerializeField] private float secretWallMoveDistance = 3f; 
        [SerializeField] private float secretWallMoveDuration = 2f; 

        public bool IsSolved { get; private set; }


        private void Start()
        {
            foreach (var skull in rotatableSkulls)
            {
                skull.OnStateChanged.AddListener(CheckSolution);
            }
        }

        public void CheckSolution()
        {
            if (IsSolved) return;

            for (int i = 0; i < rotatableSkulls.Count; i++)
            {
                if (!rotatableSkulls[i].IsTurnedRight())
                    return;
            }

            IsSolved = true;
            OnPuzzleSolved();
        }

        public void OnPuzzleSolved()
        {
            Debug.Log("Skulls puzzle solved!");

            if (secretWall != null)
            {
                Vector3 targetPos = secretWall.position + Vector3.down * secretWallMoveDistance;
                secretWall.DOMove(targetPos, secretWallMoveDuration).SetEase(Ease.InOutSine);
            }
        }
    }
}
