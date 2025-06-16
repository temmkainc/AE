using UnityEngine;

namespace AE
{
    public interface IHoldable
    {
        void OnPickup(Transform handTransform) { }
        void OnPickup(Transform handTransform, Quaternion rotation) { }
        void OnDrop();

        bool CanHold();
    }
}
