using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public sealed class Barn : MonoBehaviour
    {
        [SerializeField]
        private int resourceCapacity = 50; 
        
        [ShowInInspector, ReadOnly]
        private int resourceAmount;

        public void AddResources(int range)
        {
            if (this.CanAddResources(range))
            {
                this.resourceAmount += range;
            }
        }

        public bool CanAddResources(int range)
        {
            return this.resourceAmount + range <= this.resourceCapacity;
        }

        public int CapacityLeft()
        {
            return this.resourceCapacity - this.resourceAmount;
        }

        public bool IsFull()
        {
            return this.resourceAmount >= this.resourceCapacity;
        }

        [Button]
        public void Clear()
        {
            this.resourceAmount = 0;
        }
    }
}