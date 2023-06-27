using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;

namespace Models.Declarative.Weapons
{
    public class ReloadModel : IDisposable
    {
        public readonly AtomicVariable<float> ReloadDelay = new AtomicVariable<float>();
        public readonly AtomicVariable<float> ReloadTimer = new AtomicVariable<float>();
        public readonly AtomicAction OnReload;
        public readonly AtomicEvent<float> ReloadStarted = new AtomicEvent<float>();
        public readonly AtomicVariable<bool> IsReloading = new AtomicVariable<bool>();
        private ClipModel _clipModel;
        private bool _constructed;

        public ReloadModel()
        {
            OnReload = new AtomicAction(()=>
            {
                IsReloading.Value = true;
                ReloadStarted.Invoke(ReloadDelay.Value);
            });
        }

        public void Construct(ClipModel clipModel)
        {
            _clipModel = clipModel;
            _constructed = true;
        }

        public void Update(float dt)
        {
            if(!_constructed)
                return;

            if (IsReloading.Value)
            {
                ReloadTimer.Value += dt;
                if (ReloadTimer.Value > ReloadDelay.Value)
                {
                    _clipModel.ShotsLeft.Value = _clipModel.ClipSize.Value;
                    ReloadTimer.Value = 0;
                    IsReloading.Value = false;
                }
            }

            if (_clipModel.ShotsLeft.Value <= 0 && !IsReloading.Value)
                OnReload.Invoke();
        }

        public void Dispose()
        {
        }

        public void CancelReload()
        {
            IsReloading.Value = false;
            ReloadTimer.Value = 0f;
        }
    }
}