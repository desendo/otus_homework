using System.Collections.Generic;
using Custom.DependencyInjection;
using UnityEngine;

namespace Custom.GameManager
{
    public class UpdateManager : MonoBehaviour, IInit
    {
        private List<IUpdate> _updates;
        private List<IFixedUpdate> _fixedUpdates;
        private bool _isInitialized;

        [Inject]
        public void Construct(List<IUpdate> updateListeners, List<IFixedUpdate> fixedUpdateListeners)
        {
            _updates = updateListeners;
            _fixedUpdates = fixedUpdateListeners;
        }

        private void FixedUpdate()
        {
            if (!_isInitialized) return;

            foreach (var fixedUpdate in _fixedUpdates)
                fixedUpdate.FixedUpdate(Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (!_isInitialized) return;

            foreach (var update in _updates)
                update.Update(Time.deltaTime);
        }

        public void Init()
        {
            _isInitialized = true;
        }
    }
}