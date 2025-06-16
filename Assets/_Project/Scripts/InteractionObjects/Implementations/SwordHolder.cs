using UnityEngine;
using UnityEngine.Audio;

namespace AE
{
    public class SwordHolder : MonoBehaviour, IHolder, IInteractable
    {

        private OutlineObject outline;

        [SerializeField] Sword swordToHeld;
        private IHoldable heldObject;

        [SerializeField] private float yOffset;

        [SerializeField] Transform holdPosition;

        private void Awake()
        {
            outline = GetComponent<OutlineObject>();

        }

        private void Start()
        {
            if (swordToHeld != null)
            {
                HoldObject(swordToHeld);
            }
        }

        public bool CanHold(IHoldable holdable)
        {
            Debug.Log("I can hold a sword");
            return heldObject == null && holdable is Sword;
        }

        public void HoldObject(IHoldable holdable)
        {
            heldObject = holdable;

            var holdableTransform = (holdable as MonoBehaviour)?.transform;
            if (holdableTransform != null)
            {
                holdableTransform.SetParent(holdPosition);
                holdableTransform.localPosition = new Vector3(0f,0f + yOffset,0f);

                holdableTransform.GetComponent<Sword>().isInHolder = true;
                

                holdableTransform.localRotation = Quaternion.Euler(-90f, 0f, 0f);

                var rb = holdableTransform.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = true;
            }

        }

        public IHoldable ReleaseObject()
        {
            var temp = heldObject;
            var holdableTransform = (temp as MonoBehaviour)?.transform;
            holdableTransform.GetComponent<Sword>().isInHolder = false;
            heldObject = null;
            temp.OnDrop();
            return temp;
        }



        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnHighlight()
        {
            Debug.Log("I see a holder");
            outline?.EnableOutline(true);
        }

        public void OnUnhighlight()
        {
            Debug.Log("I don`t see a holder");
            outline?.EnableOutline(false);

        }
        public void Interact()
        {
            if(heldObject != null)
                ReleaseObject();
        }

        public bool CanInteract()
        {
            return true;
        }
    }
}
