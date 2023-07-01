using Common.Atomic.Values;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField] private GameObject _fillComplete;
        [SerializeField] private Image _progress;
        [SerializeField] private TMP_Text _text;

        public void SetFill(float fill)
        {
            _progress.fillAmount = fill;

        }
        public void SetText(string text)
        {
            _text.text = text;
        }
        public void SetFillComplete(bool isComplete)
        {
            if(_fillComplete!= null)
                _fillComplete.SetActive(isComplete);
        }


    }
}