using System;
using Common.Atomic.Actions;

namespace Models.Components
{
    public sealed class Component_SetActive
    {
        private readonly Action<bool> _setActive;
        public Component_SetActive(Action<bool> setActive)
        {
            _setActive = setActive;
        }

        public void SetActive(bool isActive)
        {
            _setActive?.Invoke(isActive);
        }
    }
}