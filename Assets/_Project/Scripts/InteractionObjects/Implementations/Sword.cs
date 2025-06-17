using UnityEngine;

namespace AE
{
    public class Sword : MonoBehaviour, IInteractable, IHoldable
    {

        [SerializeField] private AudioClip noteSound, dropSound;
        [SerializeField] private int noteID;
        
 private AudioSource audioSource;
        private OutlineObject outline;
       
        private Rigidbody rb;

        public bool isInHolder;
        [SerializeField] Quaternion holdRotation;

        private void Start()
        {
            outline = GetComponent<OutlineObject>();
            audioSource = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody>();
        }


        public void OnHighlight()
        {
            Debug.Log("I see a sword");
            outline?.EnableOutline(true);
        }

        public void OnUnhighlight() {
            Debug.Log("I don`t see a sword");
            outline?.EnableOutline(false);

        }
        public void Interact() {
            if (isInHolder)
            {
                if (!audioSource.isPlaying)
                    audioSource.PlayOneShot(noteSound);

                MelodyPuzzle.instance.OnSwordHit(noteID);
            }
        }
        public void OnPickup(Transform handTransform)
        {
            rb.isKinematic = true;
            transform.SetParent(handTransform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = holdRotation;
        }

        public void OnDrop()
        {
            transform.SetParent(null);
            audioSource.PlayOneShot(dropSound);
            rb.isKinematic = false;
        }

        public bool CanHold()
        {
            return !isInHolder;
        }

        public bool CanInteract()
        {
            return isInHolder;
        }

    }
}
