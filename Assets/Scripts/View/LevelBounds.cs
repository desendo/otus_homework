using System;
using UnityEngine;

namespace View
{
    public class LevelBounds : MonoBehaviour
    {
        [SerializeField] private Transform _top;
        [SerializeField] private Transform _bottom;
        [SerializeField] private Transform _left;
        [SerializeField] private Transform _right;
        public float MaxX { get; private set; }
        public float MinX{ get; private set; }
        public float MaxZ{ get; private set; }
        public float MinZ{ get; private set; }

        public float H{ get; private set; }
        public float W{ get; private set; }

        private void Awake()
        {
            MaxX = _right.position.x;
            MinX = _left.position.x;
            MaxZ = _top.position.z;
            MinZ = _bottom.position.z;
            H = MaxZ - MinZ;
            W = MaxX - MinX;
        }
    }
}