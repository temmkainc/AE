using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AE
{
    public class CandleStand : MonoBehaviour, IInteractable
    {

        private OutlineObject outline;

        [SerializeField] private GameObject lightenedUpPart;

        public bool isLit;

        public UnityEvent OnStateChanged;


        private void Awake()
        {
            outline = GetComponent<OutlineObject>();
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
            OnStateChanged?.Invoke();
        }

        public bool CanInteract()
        {
            return true;
        }
    }
}
