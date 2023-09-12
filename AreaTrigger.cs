namespace Interactable_System
{
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class AreaTrigger : InteractionTrigger
    {
        public override UnityEvent TriggerCatalyst => _triggerCatalyst;
        private UnityEvent _triggerCatalyst = new();


        private void OnTriggerEnter(Collider other)
        {
            // if (!player) return

            SetTrigger();
        }


        public override void SetTrigger()
        {
            _triggerCatalyst.Invoke();

            gameObject.SetActive(false);
        }
    }
}