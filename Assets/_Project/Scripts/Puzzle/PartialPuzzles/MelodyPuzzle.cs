using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AE
{
    public class MelodyPuzzle : MonoBehaviour, IPuzzle
    {
        public static MelodyPuzzle instance;
       
        public bool IsSolved { get; private set; }

        public List<int> correctMelody = new List<int> { 1, 3, 4, 0, 2 };
        private List<int> playerInput = new List<int>();

        [SerializeField] private float inputResetTime = 1.5f;
        private float inputTimer;
        private bool isCountingDown = false;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Update()
        {
            if (!IsSolved && isCountingDown)
            {
                inputTimer -= Time.deltaTime;
                if (inputTimer <= 0f)
                {
                    Debug.Log("Input timeout. Resetting melody input.");
                    playerInput.Clear();
                    isCountingDown = false;
                }
            }
        }

        public void OnSwordHit(int noteID)
        {
            if (IsSolved) return;

            playerInput.Add(noteID);

            inputTimer = inputResetTime;
            isCountingDown = true;

            if (playerInput.Count == correctMelody.Count)
            {
                CheckSolution();
            }
        }

        public void CheckSolution()
        {
            for (int i = 0; i < correctMelody.Count; i++)
            {
                if (playerInput[i] != correctMelody[i])
                {
                    Debug.Log("Wrong melody. Try again.");
                    playerInput.Clear();
                    isCountingDown = false;
                    return;
                }
            }

            IsSolved = true;
            isCountingDown = false;
            OnPuzzleSolved();
        }

        public void OnPuzzleSolved()
        {
            Debug.Log("Melody Puzzle Solved!");
        }
    }
}
