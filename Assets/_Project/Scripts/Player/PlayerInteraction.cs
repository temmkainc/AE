using UnityEngine;

namespace AE
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float maxInteractDistance = 3f;
        [SerializeField] private LayerMask interactableMask;

        [SerializeField] private Transform handTransform;

        private IInteractable currentInteractable;
        private IHoldable heldObject;

        void Update()
        {
            HandleRaycast();
            HandleInteractionInput();
            HandlePickupDropInput();
        }

        private void HandleRaycast()
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.DrawRay(ray.origin, ray.direction * maxInteractDistance, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit, maxInteractDistance, interactableMask))
            {
                var interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    if (heldObject == null || interactable is IHolder)
                    {
                        if (interactable != currentInteractable)
                        {
                            ClearCurrentInteractable();
                            currentInteractable = interactable;
                            currentInteractable.OnHighlight();
                        }
                    } 

                    SetUIHintInfo(interactable);
                    return;
                }
            }

            ClearCurrentInteractable();
            if (heldObject == null)
            {
                UIManager.instance.ResetHints();
            }
            else
            {
                UIManager.instance.interactInfo = UIManager.InteractInfo.None;
                UIManager.instance.pickupInfo = UIManager.PickupInfo.Drop;
                UIManager.instance.SetHintOnInteraction();
            }
        }


        private void ClearCurrentInteractable()
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnUnhighlight();
                currentInteractable = null;
            }
        }

        private void HandleInteractionInput()
        {

            if (heldObject != null && currentInteractable is IHolder holder)
            {
                if (holder.CanHold(heldObject) && Input.GetMouseButtonDown(0))
                {
                    holder.HoldObject(heldObject);
                    heldObject = null;
                    ClearCurrentInteractable();
                    return;
                }
            }

            if (currentInteractable != null && Input.GetMouseButtonDown(0))
            {
                currentInteractable.Interact();
            }
        }

        private void HandlePickupDropInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (heldObject != null)
                {
                    heldObject.OnDrop();
                    heldObject = null;
                }
                else if (currentInteractable is IHoldable holdable)
                {
                    if (currentInteractable is Sword sword && sword.isInHolder) return;
                    holdable.OnPickup(handTransform);
                    heldObject = holdable;
                    ClearCurrentInteractable();
                }

            }
        }

        private void SetUIHintInfo(IInteractable interactable)
        {
            if (heldObject != null)
            {
                if (interactable is IHolder holder)
                {
                    if (interactable.CanInteract())
                    {
                        UIManager.instance.interactInfo = UIManager.InteractInfo.Interact;
                    }
                    else
                    {
                        UIManager.instance.interactInfo = UIManager.InteractInfo.None;
                    }
                }
                else
                {
                    UIManager.instance.interactInfo = UIManager.InteractInfo.None;
                }

                UIManager.instance.pickupInfo = UIManager.PickupInfo.Drop;
            }
            else
            {
                UIManager.instance.interactInfo = interactable.CanInteract()
                    ? UIManager.InteractInfo.Interact
                    : UIManager.InteractInfo.None;

                if (interactable is IHoldable holdable)
                {
                    UIManager.instance.pickupInfo = holdable.CanHold()
                        ? UIManager.PickupInfo.Pickup
                        : UIManager.PickupInfo.None;
                }
                else
                {
                    UIManager.instance.pickupInfo = UIManager.PickupInfo.None;
                }
            }

            UIManager.instance.SetHintOnInteraction();
        }


    }
}
