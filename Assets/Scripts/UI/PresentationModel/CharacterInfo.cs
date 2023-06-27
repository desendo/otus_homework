using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.PresentationModel
{
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
    public sealed class CharacterInfo
    {
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;

        private readonly HashSet<CharacterStat> stats = new HashSet<CharacterStat>();

        public void AddStat(CharacterStat stat)
        {
            if (this.stats.Add(stat))
            {
                this.OnStatAdded?.Invoke(stat);
            }
        }

        public void RemoveStat(CharacterStat stat)
        {
            if (this.stats.Remove(stat))
            {
                this.OnStatRemoved?.Invoke(stat);
            }
        }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in this.stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStat[] GetStats()
        {
            return this.stats.ToArray();
        }
    }
}