using UnityEngine;

namespace DependencyInjection.Util
{
    public static class DependencyContainerExt
    {
        public static T InstantiateInject<T>(this DependencyContainer container, T prefab) where T : Object
        {
            return InstantiateUtil.Instantiate<T>(prefab, container.Inject);
        }
    }
}