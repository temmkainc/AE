using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace AE
{
    public class RotatableSkull : MonoBehaviour, IInteractable
    {
        private OutlineObject outline;
        private AudioSource audioSource;

        public UnityEvent OnStateChanged;
        public enum Direction { North, East, South, West }

        [SerializeField] private Direction currentDirection = Direction.North;
        [SerializeField] private Direction neededDirection;
        [SerializeField] private float rotationDuration = 0.5f;
        [SerializeField] private Ease rotationEase = Ease.OutBack;

        [SerializeField] AudioClip voiceline;
        [SerializeField] string captions;
        [SerializeField] private float voiceCooldown = 8f;
        private float lastVoiceTime = -Mathf.Infinity;

        private bool isRotating = false;

        private void Awake()
        {
            outline = GetComponent<OutlineObject>();
            audioSource = GetComponent<AudioSource>();
        }

        public void OnHighlight()
        {
            outline?.EnableOutline(true);
        }

        public void OnUnhighlight()
        {
            outline?.EnableOutline(false);
        }

        public void Interact()
        {
            if (isRotating) return;
            isRotating = true;

            currentDirection = (Direction)(((int)currentDirection + 1) % 4);

            float targetYRotation = (int)currentDirection * 90f;

            transform
                .DOLocalRotate(new Vector3(0, targetYRotation, 0), rotationDuration)
                .SetEase(rotationEase)
                .OnComplete(() => isRotating = false);

            OnStateChanged?.Invoke();

        }

        public bool CanInteract()
        {
            return true;
        }

        public bool IsTurnedRight()
        {
            return currentDirection == neededDirection;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (Time.time - lastVoiceTime >= voiceCooldown)
                {
                    lastVoiceTime = Time.time;
                    audioSource.PlayOneShot(voiceline);
                    UIManager.instance.SetCaptions(captions, voiceline.length + 0.5f);
                }
            }
        }
    }
}
