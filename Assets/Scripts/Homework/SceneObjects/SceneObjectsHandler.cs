using System.Collections.Generic;
using Homework.Data;
using Pool;
using UnityEngine;

namespace Homework.SceneObjects
{
    public abstract class SceneObjectsHandler<TView, TData, TDataComponent> : MonoBehaviour, ISaveLoader, ISpawner
        where TView : MonoBehaviour, ISceneElement
        where TDataComponent : MonoBehaviour
        where TData : DataElement, IViewData, new()
    {
        [SerializeField] private Transform _parent;
        protected PoolViewByIdBase<TView> _pool;
        private List<TData> _data;


        public bool TryLoad(ISaveDataProvider saveDataProvider)
        {
            if (saveDataProvider.TryGetData<List<TData>>(out var data))
            {
                _data = data;
                return true;
            }

            _data = GetSceneData();
            return true;
        }

        private List<TData> GetSceneData()
        {
            var data = new List<TData>();
            var dataComponents = _parent.GetComponentsInChildren<TDataComponent>();
            foreach (var dataObject in dataComponents)
            {
                var element = new TData();

                InitializeElementData(element, dataObject);
                var viewComponent = dataObject.GetComponent<TView>();
                element.Id = viewComponent.Id;
                element.X = viewComponent.transform.position.x;
                element.Y = viewComponent.transform.position.z;
                element.Rotation = viewComponent.transform.eulerAngles.y;
                element.ViewId = viewComponent.ViewId;
                data.Add(element);
            }

            return data;
        }

        public void Save(ISaveDataProvider saveDataProvider)
        {
            var data = GetSceneData();
            saveDataProvider.SetData(data);
        }

        protected virtual void InitializeElementData(TData dataElement, TDataComponent unitObject)
        {

        }
        protected virtual void InitializeObject(TData dataElement, TDataComponent unitObject)
        {

        }

        public void Spawn()
        {
            Clear();

            if (_data == null) return;

            foreach (var element in _data)
            {
                var instance =  _pool.Spawn(element.ViewId, _parent);
                instance.ViewId = element.ViewId;
                instance.Id = element.Id;
                instance.transform.position = new Vector3(element.X,0,element.Y);
                instance.transform.eulerAngles = new Vector3(0,element.Rotation, 0);

                InitializeObject(element, instance.GetComponent<TDataComponent>());
            }
        }

        private void Clear()
        {
            var children = transform.GetComponentsInChildren<TView>();
            foreach (var obj in children)
            {
                _pool.Unspawn(obj);
            }
        }
    }
}
