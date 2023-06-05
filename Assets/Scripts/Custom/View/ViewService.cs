using System;
using System.Collections.Generic;
using Custom.View.UI;

namespace Custom.View
{
        public interface IViewService : IReset
    {
        public void Show<T>(object args = null);
        public void Hide<T>();
        public void HideAll();
        public void RegisterOpener(IViewOpener opener);
    }
    public class ViewService : IViewService
    {
        private readonly Dictionary<Type, IViewOpener> _viewOpeners = new Dictionary<Type, IViewOpener>();
        public void Show<T>(object args = null)
        {
            var type = typeof(T);
            if(!_viewOpeners.ContainsKey(type))
                throw new Exception($"cant find handlers for type {type}");

            var opener = _viewOpeners[type];
            opener.Show(type, args);
        }

        public void Hide<T>()
        {
            var type = typeof(T);
            if(!_viewOpeners.ContainsKey(type))
                throw new Exception($"cant find handlers for type {type}");

            var opener = _viewOpeners[type];
            opener.Hide(type);
        }

        public void HideAll()
        {
            foreach (var (type, opener) in _viewOpeners)
            {
                opener.Hide(type);
            }
        }

        public void RegisterOpener(IViewOpener opener)
        {
            foreach (var openerViewType in opener.ViewTypes)
            {
                if(!_viewOpeners.TryAdd(openerViewType, opener))
                    throw new Exception($"type {openerViewType} already add to dictionary");
            }
        }

        public void Reset()
        {
            HideAll();
        }
    }
}