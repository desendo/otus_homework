using System;
using UI.PresentationModel;

namespace Custom.View.PresentationModel
{
    public interface INameValuePresentationModel : IDisposable
    {
        string GetName();
        int GetValue();
        event Action<int> OnValueChanged;
    }
    public class CharacterStatPresentationModel : INameValuePresentationModel
    {
        private readonly CharacterStat _characterStat;
        public event Action<int> OnValueChanged;

        public CharacterStatPresentationModel(CharacterStat characterStat)
        {
            _characterStat = characterStat;
            characterStat.OnValueChanged += OnChangeValue;
        }

        private void OnChangeValue(int obj)
        {
            OnValueChanged?.Invoke(obj);
        }

        public string GetName()
        {
            return _characterStat.Name;
        }

        public int GetValue()
        {
            return _characterStat.Value;
        }

        public void Dispose()
        {
            _characterStat.OnValueChanged -= OnChangeValue;
        }
    }
}