using System;
using System.Collections.Generic;
using Pool;
using TMPro;
using UI.PresentationModel;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponUIView : MonoBehaviour, ISpawn, IDisposable
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _shots;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private Image _reload;
        [SerializeField] private GameObject _active;

        private readonly List<IDisposable> _subscriptions = new List<IDisposable>();

        public void Setup(WeaponPresentationModel pm)
        {
            _subscriptions.Add(pm.Name.OnChanged.Subscribe(x => _name.text = x));
            _name.text = pm.Name.Value;

            _subscriptions.Add(pm.Shots.OnChanged.Subscribe(x => _shots.text = x));
            _shots.text = pm.Shots.Value;

            _subscriptions.Add(pm.Damage.OnChanged.Subscribe(x => _damage.text = x));
            _damage.text = pm.Damage.Value;

            _subscriptions.Add(pm.ReloadProgress.OnChanged.Subscribe(x => _reload.fillAmount = x));
            _reload.fillAmount = pm.ReloadProgress.Value;

            _subscriptions.Add(pm.IsActive.OnChanged.Subscribe(x => _active.gameObject.SetActive(x)));
            _active.gameObject.SetActive(pm.IsActive.Value);
        }

        public void OnSpawn()
        {
        }

        public void OnUnSpawn()
        {
            Dispose();
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription?.Dispose();
            }
            _subscriptions.Clear();
        }
    }
}