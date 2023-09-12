namespace Interactable_System
{
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Interactable : MonoBehaviour, IInteractable
    {
        private Transform _transform;

        public Vector3 Position => _transform.position;

        public virtual bool IsDialogue => false;
        public virtual bool IsPickup => false;

        public bool IsUsed => _isUsed;
        private bool _isUsed;

        [SerializeField] protected InteractionTrigger trigger;

        [Space(10), SerializeField] protected bool needItem;
        // Request item required for interactable to activate

        [Space(10), SerializeField] protected bool hasOneUse;
        [SerializeField] protected bool startUsed;

        [Space(10), SerializeField] protected GameObject targetIllustration;

        [Space(10), SerializeField] private Material highlightMaterial;
        [SerializeField] private Material targetMaterial;

        private MeshRenderer[] _allRenderers;
        private Dictionary<MeshRenderer, Material[]> _defaultMaterials;

        private bool _isIllustrating;


        protected virtual void Awake()
        {
            _transform = transform;

            targetIllustration.SetActive(false);
            _isIllustrating = false;

            _allRenderers = GetComponentsInChildren<MeshRenderer>(false);
            _defaultMaterials = new Dictionary<MeshRenderer, Material[]>();

            foreach (var r in _allRenderers)
                _defaultMaterials.Add(r, r.materials);

            _isUsed = startUsed;
        }


        public virtual void Highlight(bool isTarget)
        {
            ChangeAllMaterials(isTarget ? targetMaterial : highlightMaterial);

            if (!_isIllustrating && isTarget)
            {
                _isIllustrating = true;
                targetIllustration.SetActive(true);
            }

            else if (_isIllustrating && !isTarget)
            {
                targetIllustration.SetActive(false);
                _isIllustrating = false;
            }
        }

        public virtual void RemoveHighlight()
        {
            ResetAllMaterials();

            targetIllustration.SetActive(false);
            _isIllustrating = false;
        }


        private void ChangeAllMaterials(Material material)
        {
            for (var r = 0; r < _allRenderers.Length; r++)
            {
                var newMaterials = _allRenderers[r].materials;

                for (var m = 0; m < newMaterials.Length; m++)
                {
                    newMaterials[m] = material;
                }

                _allRenderers[r].materials = newMaterials;
            }
        }

        private void ResetAllMaterials()
        {
            foreach (var r in _allRenderers)
            {
                r.materials = _defaultMaterials[r];
            }
        }


        public void ActivateInteractable()
        {
            _isUsed = false;
        }

        public virtual void Interact()
        {
            if (_isUsed)
                return;

            if (needItem) // && player does not have item
                return;

            if (hasOneUse)
                _isUsed = true;

            trigger.SetTrigger();

            IllustrateInteraction();
        }

        protected virtual void IllustrateInteraction()
        {
            targetIllustration.SetActive(true);
            targetIllustration.GetComponent<InteractionIllustration>().BeginInteraction();
        }
    }


    public interface IInteractable
    {
        Vector3 Position { get; }

        bool IsPickup { get; }
        bool IsDialogue { get; }

        bool IsUsed { get; }

        void Highlight(bool isTarget);
        void RemoveHighlight();
        void Interact();
    }
}