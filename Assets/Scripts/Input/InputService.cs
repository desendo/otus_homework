using Common.Atomic.Actions;
using UnityEngine;

namespace Input
{
    public enum FireState
    {
        None,
        Pressed,
        IsPressing,
        Released
    }

    public sealed class InputService : IUpdate
    {
        public readonly AtomicEvent<FireState> FireState = new AtomicEvent<FireState>();
        public readonly AtomicEvent<int> DigitPressed = new AtomicEvent<int>();
        public readonly AtomicEvent ReloadPressed = new AtomicEvent();
        public Vector2 MouseScreenPosition { get; private set; }
        public Vector2 MoveDirection { get; private set; }

        public void Update(float dt)
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
                DigitPressed.Invoke(1);
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
                DigitPressed.Invoke(2);
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
                DigitPressed.Invoke(3);
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
                DigitPressed.Invoke(4);

            if(UnityEngine.Input.GetKeyDown(KeyCode.R))
                ReloadPressed.Invoke();

            if(UnityEngine.Input.GetMouseButtonDown(0))
                FireState.Invoke(Input.FireState.Pressed);
            else if(UnityEngine.Input.GetMouseButton(0))
                FireState.Invoke(Input.FireState.IsPressing);
            else if(UnityEngine.Input.GetMouseButtonUp(0))
                FireState.Invoke(Input.FireState.Released);
            else
                FireState.Invoke(Input.FireState.None);

            MouseScreenPosition = UnityEngine.Input.mousePosition;

            var dir = Vector2.zero;

            if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
                dir.y = 1;
            else if (UnityEngine.Input.GetKey(KeyCode.DownArrow))
                dir.y = -1;
            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                dir.x = -1;
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                dir.x = 1;
            
            var x = UnityEngine.Input.GetAxis("Horizontal");
            var y = UnityEngine.Input.GetAxis("Vertical");
            MoveDirection = new Vector2(x,y);


            //MoveDirection = dir;

        }
    }
}