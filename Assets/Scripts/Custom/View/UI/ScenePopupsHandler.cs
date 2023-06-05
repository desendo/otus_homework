using System;
using System.Collections.Generic;
using Custom.DependencyInjection;
using Custom.View.UI.Popup;
using UnityEngine;

namespace Custom.View.UI
{
    public interface IViewOpener
    {
        public HashSet<Type> ViewTypes { get; }
        public void Show(Type type, object args);
        public void Hide(Type type);
    }
    public class ScenePopupsHandler : MonoBehaviour, IViewOpener
    {
        private readonly List<PopupTypeData> _popupsToHandle = new List<PopupTypeData>();
        private readonly HashSet<object> _openedViews = new HashSet<object>();

        private IViewService _viewService;
        private DependencyContainer _dependencyContainer;

        public HashSet<Type> ViewTypes { get; } = new HashSet<Type>();

        [Inject]
        public void Construct(DependencyContainer dependencyContainer, IViewService viewService)
        {
            _dependencyContainer = dependencyContainer;
            _viewService = viewService;

            InitializePopups();
            InjectPopups();

            _viewService.RegisterOpener(this);
        }

        private void InitializePopups()
        {
            var popups = transform.GetComponentsInChildren<PopupBase>(true);
            foreach (var popup in popups)
            {
                var type = popup.GetType();
                _popupsToHandle.Add(new PopupTypeData() {Instance = popup, Type = type});
                ViewTypes.Add(type);
                popup.gameObject.SetActive(false);
            }
        }

        private void InjectPopups()
        {
            foreach (var popupTypeData in _popupsToHandle)
            {
                _dependencyContainer.Inject(popupTypeData.Instance);
            }
        }

        public void Show(Type type, object args)
        {
            if(!ViewTypes.Contains(type))
                throw new Exception($"{GetType()} {name} does not handle type {type}");



            foreach (var popupTypeData in _popupsToHandle)
            {
                if (popupTypeData.Type == type && popupTypeData.Instance is IView view)
                {
                    if (_openedViews.Contains(view))
                    {
                        Debug.Log($"already opened {view}");
                        return;
                    }
                    view.Show(()=> Hide(type), args);
                    _openedViews.Add(view);
                    break;
                }
            }
        }

        public void Hide(Type type)
        {
            if (!ViewTypes.Contains(type))
                throw new Exception($"{GetType()} {name} does not handle type {type}");

            foreach (var popupTypeData in _popupsToHandle)
            {
                if (popupTypeData.Type == type && _openedViews.Contains(popupTypeData.Instance))
                {
                    if (popupTypeData.Instance is IView view)
                    {
                        view.Hide();
                        _openedViews.Remove(view);
                        break;
                    }
                    else
                        throw new Exception($"{popupTypeData.Instance} not implementing {typeof(IView)}");

                }
            }
        }
        private class PopupTypeData
        {
            public MonoBehaviour Instance;
            //решил не использовать GetType, а сделать отдельное поле
            public Type Type;
        }
    }
}
