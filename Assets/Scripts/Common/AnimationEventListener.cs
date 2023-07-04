using Common.Atomic.Actions;
using UnityEngine;

namespace Common
{
    public class AnimationEventListener : MonoBehaviour
    {
        public AtomicEvent<string> OnEvent = new AtomicEvent<string>();
        public void StringEvent(string value)
        {
            OnEvent.Invoke(value);
        }
    }
}
