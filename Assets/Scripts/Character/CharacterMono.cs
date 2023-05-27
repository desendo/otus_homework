using UnityEngine;

namespace Character
{
    public class CharacterMono : MonoBehaviour, IReset
    {
        private Vector3 _initialPosition;

        private void Awake()
        {
            _initialPosition = transform.position;
        }

        public void DoReset()
        {
            transform.position = _initialPosition;
            ResetComponents();
        }

        private void ResetComponents()
        {
            var resets = GetComponents<IReset>();
            foreach (var reset in resets)
            {
                if (reset != (IReset) this)
                    reset.DoReset();
            }
        }
    }
}