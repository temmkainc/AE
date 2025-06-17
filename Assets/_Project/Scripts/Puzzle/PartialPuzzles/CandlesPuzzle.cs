using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AE
{
    public class CandlesPuzzle : MonoBehaviour, IPuzzle
    {
        [SerializeField] private List<CandleStand> candleStands;
        [SerializeField] private List<bool> correctPattern;


        [SerializeField] private GameObject swordPrefab;
        [SerializeField] private Transform swordSpawnPoint;

        [SerializeField] private Transform chestLid;
        [SerializeField] private ChestWithSword chest;
        [SerializeField] private Vector3 openRotation = new Vector3(-75f, 0f, 0f);
        [SerializeField] private float openDuration = 2f;
        [SerializeField] private Ease openEase = Ease.OutBack;

        [SerializeField] private AudioSource chestSource;
        [SerializeField] private AudioClip chestOpen;
       

        public bool IsSolved { get; private set; }

        private void Start()
        {
            foreach (var stand in candleStands)
            {
                stand.OnStateChanged.AddListener(CheckSolution);
            }
        }

        public void CheckSolution()
        {
            if (IsSolved) return;

            for (int i = 0; i < candleStands.Count; i++)
            {
                if (candleStands[i].isLit != correctPattern[i])
                    return;
            }

            IsSolved = true;
            OnPuzzleSolved();
        }
        public void OnPuzzleSolved()
        {
            if (chestLid != null)
            {
                Debug.Log("Rotating chest lid!");
                chestLid.DOLocalRotate(openRotation, openDuration)
                    .OnStart(() => chestSource.PlayOneShot(chestOpen))
                    .SetEase(openEase)
                    .OnComplete(() => Debug.Log("Chest lid opened"));

                UIManager.instance.SetCaptions("[Chest unlocked]", 3f);
            }

            if (swordPrefab != null && swordSpawnPoint != null)
            {
                Instantiate(swordPrefab, swordSpawnPoint.position, swordSpawnPoint.rotation);
                chest.OnChestOpened();

                swordPrefab.GetComponent<Sword>();
            }

            Debug.Log("Candle puzzle solved!");
        }
    }
}
