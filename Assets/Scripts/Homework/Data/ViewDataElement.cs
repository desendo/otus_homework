using UnityEngine;

namespace Homework.Data
{
    public class ViewDataElement : DataElement
    {
        public string ViewId { get => _viewData.ViewId; set => _viewData.ViewId = value; }
        public float X { get => _viewData.X; set => _viewData.X = value; }
        public float Y { get => _viewData.Y; set => _viewData.Y = value; }
        public float Rotation { get => _viewData.Rotation; set => _viewData.Rotation = value; }

        private readonly ViewData _viewData = new ViewData();
    }
}