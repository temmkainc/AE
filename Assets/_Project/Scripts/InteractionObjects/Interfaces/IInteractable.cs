using UnityEngine;

namespace AE
{
    public interface IInteractable
    {
        public void OnHighlight();
        public void OnUnhighlight();
        public void Interact();

        bool CanInteract();
    }
}
