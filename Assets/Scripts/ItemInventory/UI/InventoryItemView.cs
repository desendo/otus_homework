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
        [SerializeField] private CanvasGroup _descriptionContainer;
        private float _timer;
        private bool _hover;
        private readonly float _hoverDelay = 0.5f;
        private readonly List<IDisposable> _subs = new List<IDisposable>();
        private bool _isDragging;
        private RectTransform _rectTransform;
        private Vector3 _lastPosition;
        private ItemPresentationModel _pm;
        private Image _image;
        private Vector3 _savedPosition;
        private Transform _savedParent;
        private Transform _dragContainer;
        private bool _inSlot;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
        }

        public void Setup(ItemPresentationModel pm, Transform dragContainer)
        {
            HideDescription();
            _dragContainer = dragContainer;
            _pm = pm;
            _count.text = pm.Count.Value;
            _name.text = pm.Name.Value;
            _icon.sprite = pm.Icon.Value;
            _metaInfo.text = pm.Description.Value;
        }

        private void Update()
        {
            if (_hover && !_isDragging)
            {
                _timer += Time.deltaTime;
                if (_timer > _hoverDelay)
                {
                    ShowDescription();
                }
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
            _descriptionContainer.alpha = 1f;
            _descriptionContainer.gameObject.SetActive(true);
        }

        void HideDescription()
        {
            _descriptionContainer.alpha = 0f;
            _descriptionContainer.gameObject.SetActive(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _rectTransform.position += (Vector3)eventData.delta;
            _image.raycastTarget = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            HideDescription();
            _savedPosition = transform.position;
            _savedParent = transform.parent;
            _image.raycastTarget = false;
            _lastPosition = _rectTransform.position;
            _rectTransform.SetParent(_dragContainer);
            _pm.SetDragging(true);

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _image.raycastTarget = true;
            _isDragging = false;
            if(!_inSlot)
                ResetTransform();
            _pm.SetDragging(false);
        }

        public void ResetTransform()
        {
            transform.position = _savedPosition;
            transform.SetParent(_savedParent);
        }

        public void SetIsInSlot(bool inSlot)
        {
            _inSlot = inSlot;
        }
    }
}