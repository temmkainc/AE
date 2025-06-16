using UnityEngine;

namespace AE
{
    public interface IHolder
    {
        bool CanHold(IHoldable holdable);
        void HoldObject(IHoldable holdable);
        IHoldable ReleaseObject();
    }
}
