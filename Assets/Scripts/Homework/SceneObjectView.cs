using UnityEditor;
using UnityEngine;

namespace Homework
{
    public class SceneObjectView : MonoBehaviour, ISceneElement
    {
        [SerializeField] private string _viewId;
        [SerializeField] private string _unitId;

        public string ViewId
        {
            get => _viewId;
            set => _viewId = value;
        }

        public string Id
        {
            get => _unitId;
            set => _unitId = value;
        }

        [ContextMenu("Generate Ids")]
        //вызываем после помещения объекта на сцену каким-либо способом
        public void GenerateIds()
        {
            _unitId = gameObject.GetInstanceID().ToString();
            _viewId = PrefabUtility.GetCorrespondingObjectFromSource(gameObject)?.name;
        }
    }
    public interface IView
    {
        public string ViewId { get; set; }
    }
    public interface ISceneElement : IView
    {
        public string Id { get; set; }
    }
}