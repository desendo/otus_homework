using System;

namespace Custom.View.PresentationModel
{
    public interface IExperiencePresentationModel
    {
        public event Action OnChanged;
        bool IsComplete { get; }
        float GetProgressValue();
        string GetExperienceText();
        void LevelUp();
        string GetLevelText();
    }

    public class ExperiencePresentationModel : IExperiencePresentationModel
    {
        public event Action OnChanged;
        public bool IsComplete => _playerLevel?.RequiredExperience <= _playerLevel?.CurrentExperience;

        private readonly PlayerLevel _playerLevel;

        public ExperiencePresentationModel(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
            playerLevel.OnExperienceChanged += PlayerLevelExperienceChanged;
            playerLevel.OnLevelUp += OnLevelUp;
        }

        private void OnLevelUp()
        {
            OnChanged?.Invoke();
        }

        private void PlayerLevelExperienceChanged(int exp)
        {
            OnChanged?.Invoke();
        }
        public float GetProgressValue()
        {
            return (float)_playerLevel.CurrentExperience/_playerLevel.RequiredExperience;
        }
        public string GetExperienceText()
        {
            return $"XP: {_playerLevel.CurrentExperience}/{_playerLevel.RequiredExperience}";
        }
        public string GetLevelText()
        {
            return $"Level: {_playerLevel.CurrentLevel}";
        }
        public void LevelUp()
        {
            _playerLevel.LevelUp();
        }
    }
}