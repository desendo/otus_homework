using UnityEngine;

namespace Models.Components
{

    public sealed class Component_Transform
    {
        public Transform RootTransform { get; }
        public Component_Transform(Transform transform)
        {
            RootTransform = transform;
        }

        public void Translate(Vector3 moveStep)
        {
            if(RootTransform != null)
                RootTransform.position += moveStep;
        }
    }
}