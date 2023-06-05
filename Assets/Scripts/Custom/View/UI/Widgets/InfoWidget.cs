using System;
using Custom.View.PresentationModel;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace Custom.View.UI.Widgets
{
    public class InfoWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _decription;
        [SerializeField] private Transform _raysEffect;
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> _tween;
        private Sequence _seq;

        private void OnEnable()
        {

            _seq = DOTween.Sequence();
            _seq.Append(
                _raysEffect.DORotate(new Vector3(0, 0, 360), 10, RotateMode.FastBeyond360).SetEase(Ease.Linear))
                    .SetLoops(-1, LoopType.Incremental);

        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }
        public void SetDescription(string desc)
        {
            _decription.text = desc;
        }
        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }
        private void OnDisable()
        {
            _seq?.Kill();
        }
    }
}