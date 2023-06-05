using System;
using System.Collections.Generic;
using System.Linq;

namespace Custom.View.PresentationModel
{


    public interface ICharacterInfoPresentationModel
    {
        List<INameValuePresentationModel> StatsPmList { get; }
        event Action<INameValuePresentationModel> OnStatRemove;
        event Action<INameValuePresentationModel> OnStatAdd;
    }
    public class CharacterInfoPresentationModel : ICharacterInfoPresentationModel
    {
        public List<INameValuePresentationModel> StatsPmList { get; } = new List<INameValuePresentationModel>();
        public event Action<INameValuePresentationModel> OnStatRemove;
        public event Action<INameValuePresentationModel> OnStatAdd;

        public CharacterInfoPresentationModel(CharacterInfo characterInfo)
        {
            characterInfo.OnStatAdded += OnStatAdded;
            characterInfo.OnStatRemoved += OnOnStatRemoved;
            var stats = characterInfo.GetStats();
            foreach (var characterStat in stats)
            {
                StatsPmList.Add(new CharacterStatPresentationModel(characterStat));
            }
        }

        private void OnOnStatRemoved(CharacterStat obj)
        {
            var pm = StatsPmList.FirstOrDefault(x => x.GetName() == obj.Name);
            if (pm != null)
            {
                StatsPmList.Remove(pm);
                OnStatRemove?.Invoke(pm);
                pm.Dispose();
            }
        }

        private void OnStatAdded(CharacterStat stat)
        {
            var pm = StatsPmList.FirstOrDefault(x => x.GetName() == stat.Name);
            if (pm == null)
            {
                var newPm = new CharacterStatPresentationModel(stat);
                StatsPmList.Add(newPm);
                OnStatAdd?.Invoke(newPm);
            }
        }
    }
}