using System;
using System.Collections.Generic;
using Config;
using Custom.View.PresentationModel;

namespace UI.PresentationModel
{
    public interface ISelectableIconsPresentationModel : ISaveValuesPresentationModel
    {
        event Action<string> OnIconSelected;

        List<SpriteEntry> IconsEntries { get; }
        void SetIconSelected(string id);
        string GetCurrentIcon();
    }
}