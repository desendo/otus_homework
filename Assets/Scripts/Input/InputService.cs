using UnityEngine;

namespace Input
{
    public sealed class InputService : IUpdate
    {
        public bool Fire { get; private set; }
        public Vector2 MouseScreenPosition { get; private set; }
        public Vector2 MoveDirection { get; private set; }

        public void Update(float dt)
        {
            Fire = UnityEngine.Input.GetMouseButton(0);
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