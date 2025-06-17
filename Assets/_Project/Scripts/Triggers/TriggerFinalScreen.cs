using DG.Tweening;
using UnityEngine;


namespace AE
{
    public class TriggerFinalScreen : MonoBehaviour
    {

        [SerializeField] private GameObject finalScreen;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip finalClip;
        [SerializeField] private AudioClip metalGateClip;

        [SerializeField] private Transform finalWall;
        [SerializeField] private float secretWallMoveDistance = 6f;
        [SerializeField] private float secretWallMoveDuration = 6f;

        private bool hasTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player") && !hasTriggered)
            {
                if (finalWall != null)
                {
                    Vector3 targetPos = finalWall.position + Vector3.up * secretWallMoveDistance;
                    finalWall.DOMove(targetPos, secretWallMoveDuration).SetEase(Ease.InOutSine)
                        .OnStart(() => audioSource.PlayOneShot(metalGateClip));
                }
                Invoke("ActivateBlackScreen", 7f);
                hasTriggered = true;
            }
        }

        private void ActivateBlackScreen()
        {
            finalScreen.SetActive(true);
            audioSource.PlayOneShot(finalClip);
            Invoke("CloseApplication", 10f);
        }

        private void CloseApplication()
        {
            Application.Quit();
        }

    }
}
