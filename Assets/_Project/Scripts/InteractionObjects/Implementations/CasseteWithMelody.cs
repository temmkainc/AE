using UnityEngine;

namespace AE
{
    public class CasseteWithMelody : MonoBehaviour, IInteractable
    {
        private OutlineObject outline;
        private AudioSource audioSource;
        private Rigidbody rb;

        [SerializeField] AudioClip melody;

        private void Awake()
        {
            outline = GetComponent<OutlineObject>();
            audioSource = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody>();
        }

        public bool CanInteract()
        {
            return true;
        }

        public void Interact()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(melody);
            }
        }

        public void OnHighlight()
        {
            outline?.EnableOutline(true);
        }

        public void OnUnhighlight()
        {
            outline?.EnableOutline(false);
        }

    }
}
