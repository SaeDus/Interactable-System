namespace Interactable_System
{
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class InteractionTrigger : MonoBehaviour
    {
        public abstract UnityEvent TriggerCatalyst { get; }

        public abstract void SetTrigger();
    }
}