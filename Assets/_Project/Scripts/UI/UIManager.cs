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

        public Text interactionHint;

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

            // Add Interact hint
            if (interactInfo == InteractInfo.Interact)
            {
                sb.Append("[LMB] Interact\n");
            }

            // Add Pickup/Drop hint
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

        public void ResetHints()
        {
            interactionHint.text = string.Empty;
        }
    }
}
