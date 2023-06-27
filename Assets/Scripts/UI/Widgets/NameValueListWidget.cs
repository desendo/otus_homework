using System;
using System.Collections.Generic;
using System.Linq;
using Custom.View.PresentationModel;
using UnityEngine;

namespace UI.Widgets
{

    public class NameValueListWidget : MonoBehaviour, IDisposable
    {
        [SerializeField] private NameValueWidget _prefab;
        private readonly List<NameValueWidget> _widgetList = new List<NameValueWidget>();

        readonly Dictionary<INameValuePresentationModel, NameValueWidget> _dictionary = new Dictionary<INameValuePresentationModel, NameValueWidget>();
        public void Dispose()
        {
            var list = _widgetList.ToList();
            foreach (var widget in list)
            {
                widget.Dispose();
                Destroy(widget.gameObject);
            }
            _dictionary.Clear();
            _widgetList.Clear();
        }

        public void AddWidget(INameValuePresentationModel pm)
        {
            var widget = Instantiate(_prefab, transform);
            widget.Bind(pm);
            _widgetList.Add(widget);
            _dictionary.Add(pm, widget);
        }

        public void RemoveWidget(INameValuePresentationModel pm)
        {
            if (_dictionary.ContainsKey(pm))
            {
                var widget = _dictionary[pm];
                widget.Dispose();
                _dictionary.Remove(pm);
                _widgetList.Remove(widget);
                Destroy(widget.gameObject);
            }
        }
    }
}