using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AE
{
    public class CandleStand : MonoBehaviour, IInteractable
    {

        private OutlineObject outline;

        private AudioSource audioSource;
        [SerializeField] private AudioClip lightUp, blowUp, burningLoop;
        private AudioSource loopSource;

        [SerializeField] private GameObject lightenedUpPart;

        public bool isLit;

        public UnityEvent OnStateChanged;


        private void Awake()
        {
            outline = GetComponent<OutlineObject>();
            audioSource = GetComponent<AudioSource>();

            loopSource = gameObject.AddComponent<AudioSource>();
            loopSource.loop = true;
            loopSource.spatialBlend = 1f;
            loopSource.playOnAwake = false;
        }

        public void OnHighlight()
        {
            Debug.Log("I see a candle");
            outline?.EnableOutline(true);
        }

        public void OnUnhighlight()
        {
            Debug.Log("I don`t see a candle");
            outline?.EnableOutline(false);

        }
        public void Interact()
        {
            lightenedUpPart.SetActive(!lightenedUpPart.activeSelf);
            isLit = lightenedUpPart.activeSelf;
            if (!audioSource.isPlaying)
            {
                if (isLit)
                {
                    audioSource.PlayOneShot(lightUp);

                    loopSource.clip = burningLoop;
                    loopSource.Play();
                }
                else
                {
                    audioSource.PlayOneShot(blowUp);
                    loopSource.Stop();
                }
                OnStateChanged?.Invoke();
            }
        }

        public bool CanInteract()
        {
            return true;
        }
    }
}
