using UnityEngine;

namespace AE
{
    public class ChestWithSword : MonoBehaviour, IInteractable
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip lockedSound;


        public bool isLocked;

        private OutlineObject outline;

        private void Awake()
        {
            outline = GetComponent<OutlineObject>();
            audioSource = GetComponent<AudioSource>();
        }

        public bool CanInteract()
        {
            return isLocked;
        }

        public void Interact()
        {
            UIManager.instance.SetCaptions("[Locked]", 2f);
            if(!audioSource.isPlaying)
                audioSource.PlayOneShot(lockedSound);
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
