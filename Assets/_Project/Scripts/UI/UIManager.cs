using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace AE
{
    public class UIManager : MonoBehaviour
    {
        public enum PickupInfo
        {
            None,
            Pickup,
            Drop
        }

        public enum InteractInfo
        {
            None,
            Interact
        }

        public PickupInfo pickupInfo;
        public InteractInfo interactInfo;

        public static UIManager instance;

        public Text interactionHint, captions;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void SetHintOnInteraction()
        {
            StringBuilder sb = new StringBuilder();

            if (interactInfo == InteractInfo.Interact)
            {
                sb.Append("[LMB] Interact\n");
            }

            switch (pickupInfo)
            {
                case PickupInfo.Pickup:
                    sb.Append("[E] Pick up\n");
                    break;
                case PickupInfo.Drop:
                    sb.Append("[E] Drop\n");
                    break;
            }

            interactionHint.text = sb.ToString().TrimEnd(); // remove trailing newline
        }

        public void SetCaptions(string text, float timeToDisable)
        {
            captions.text = text;
            Invoke("MakeCaptionsEmpty", timeToDisable);
        }

        private void MakeCaptionsEmpty()
        {
            captions.text = string.Empty;
        }

        public void ResetHints()
        {
            interactionHint.text = string.Empty;
        }
    }
}
