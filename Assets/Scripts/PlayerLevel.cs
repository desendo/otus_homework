using System;

public sealed class PlayerLevel
{
    public event Action OnLevelUp;
    public event Action<int> OnExperienceChanged;

    public int CurrentLevel { get; private set; }

    public int CurrentExperience { get; private set; }

    public int RequiredExperience
    {
        get { return 100 * (this.CurrentLevel + 1); }
    }

    public PlayerLevel(int currentLevel, int currentExperience)
    {
        CurrentLevel = currentLevel;
        CurrentExperience = currentExperience;
    }

    public void SetExperience(int experience)
    {
        CurrentExperience = experience;
    }
    public void SetLevel(int level)
    {
        CurrentLevel = level;
    }

    public void AddExperience(int range)
    {
        var xp = Math.Min(this.CurrentExperience + range, this.RequiredExperience);
        this.CurrentExperience = xp;
        this.OnExperienceChanged?.Invoke(xp);
    }

    public void LevelUp()
    {
        if (this.CanLevelUp())
        {
            this.CurrentExperience = 0;
            this.CurrentLevel++;
            this.OnLevelUp?.Invoke();
        }
    }

    public bool CanLevelUp()
    {
        return this.CurrentExperience == this.RequiredExperience;
    }
}