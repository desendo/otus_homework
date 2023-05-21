using System.Collections.Generic;
using DependencyInjection;
using UnityEngine;

namespace ShootEmUp
{
    public class UpdateManager : MonoBehaviour
    {
        private List<IUpdate> _updates;
        private List<IFixedUpdate> _fixedUpdates;
        private bool _isInitialized;
        [Inject]
        public void Initialize(List<IUpdate> updates, List<IFixedUpdate> fixedUpdates)
        {
            _updates = updates;
            _fixedUpdates = fixedUpdates;
            _isInitialized = true;

        }
        private void FixedUpdate()
        {
            if(_isInitialized)
                _fixedUpdates.ForEach(x=>x.FixedUpdate(Time.fixedDeltaTime));
        }
        private void Update()
        {
            if(_isInitialized)
                _updates.ForEach(x=>x.Update(Time.deltaTime));
        }
    }
}