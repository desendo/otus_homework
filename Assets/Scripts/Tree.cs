using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public sealed class Tree : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private int remainingResources = 10;

        public bool HasResources()
        {
            return this.remainingResources > 0;
        }

        [Button]
        public bool TakeResource()
        {
            if (this.remainingResources <= 0)
            {
                return false;
            }

            this.remainingResources--;

            if (this.remainingResources <= 0)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.animator.Play("Chop", -1, 0);
            }

            return true;
        }
    }
}
