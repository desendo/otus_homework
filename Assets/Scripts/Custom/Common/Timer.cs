using System;

namespace Custom.Common
{
    public class Timer
    {
        private readonly float _interval;
        private float _timer;
        public event Action OnTime;

        public Timer(float interval)
        {
            _interval = interval;
        }

        public void Update(float delta)
        {
            _timer += delta;
            if (_timer > _interval)
            {
                _timer = 0f;
                OnTime?.Invoke();
            }
        }

        public void Reset()
        {
            _timer = 0f;
        }
    }
}