using System.Collections.Generic;
using UnityEngine;

namespace Homework
{
    public sealed class PlayerResources : MonoBehaviour
    {
        private readonly Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

        public void SetResource(ResourceType resourceType, int resource)
        {
            if(!_resources.ContainsKey(resourceType))
                _resources.Add(resourceType, resource);

            else
                _resources[resourceType] = resource;
        }

        public int GetResource(ResourceType resourceType)
        {
            if (!_resources.ContainsKey(resourceType))
                return 0;

            return _resources[resourceType];
        }
    }
}