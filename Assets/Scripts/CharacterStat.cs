using System;

public sealed class CharacterStat
{
    public CharacterStat(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public event Action<int> OnValueChanged;
    public string Name { get; private set; }
    public int Value { get; private set; }

    public void ChangeValue(int value)
    {
        this.Value = value;
        this.OnValueChanged?.Invoke(value);
    }
}