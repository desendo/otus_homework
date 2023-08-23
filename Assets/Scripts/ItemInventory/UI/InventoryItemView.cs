using System;
using System.Collections.Generic;

using TMPro;
using UI.PresentationModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ItemInventory.UI
{
    public class InventoryItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _metaInfo;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private GameObject _descriptionContainer;

        private float _timer;
        private bool _hover;
        private const float HoverDelay = 0.5f;

        private readonly List<IDisposable> _subs = new List<IDisposable>();
        private RectTransform _rectTransform;
        private Image _image;
        private Vector3 _savedPosition;
        private Transform _savedParent;
        private string _id;
        private Transform _dragTransform;

        public string Id => _id;
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
        }

        public void Setup(ItemPresentationModel pm)
        {
            _count.text = pm.Count.Value;
            _name.text = pm.Name.Value;
            _icon.sprite = pm.Icon.Value;
            _metaInfo.text = pm.Description.Value;
            _id = pm.Id;
            _image.raycastTarget = true;
        }

        private void Update()
        {
            if (_hover)
            {
                _timer += Time.deltaTime;
                if (_timer > HoverDelay)
                {
                    ShowDescription();
                }
            }
            else
            {
                HideDescription();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hover = true;
            _timer = 0;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hover = false;
            _timer = 0;
            HideDescription();
        }


        void ShowDescription()
        {
            _descriptionContainer.SetActive(true);
        }

        void HideDescription()
        {
            _descriptionContainer.SetActive(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position += (Vector3)eventData.delta;
            _image.raycastTarget = false;
            transform.SetParent(_dragTransform);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            HideDescription();
            _savedPosition = transform.position;
            _savedParent = transform.parent;
            _image.raycastTarget = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _image.raycastTarget = true;
            ResetTransform();
        }

        private void ResetTransform()
        {
            transform.position = _savedPosition;
            SetParent(_savedParent);
        }

        public void SetDragContainer(Transform dragTransform)
        {
            _dragTransform = dragTransform;
        }

        public void SetParent(Transform holder)
        {
            transform.SetParent(holder);
        }

        private void OnEnable()
        {
            HideDescription();
        }
    }
}