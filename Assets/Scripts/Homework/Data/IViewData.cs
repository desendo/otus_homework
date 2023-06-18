using UnityEngine;

namespace Homework.Data
{
    public interface IViewData
    {
        public string ViewId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Rotation { get; set; }
    }
}