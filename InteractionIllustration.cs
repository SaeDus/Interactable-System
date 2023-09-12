namespace Interactable_System
{
    using UnityEngine;

    public class InteractionIllustration : MonoBehaviour
    {
        private Animator _animator;

        private static readonly int Interact = Animator.StringToHash("Interact");


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        public void BeginInteraction()
        {
            _animator.SetTrigger(Interact);
        }

        public void HideIllustration()
        {
            gameObject.SetActive(false);
        }
    }
}