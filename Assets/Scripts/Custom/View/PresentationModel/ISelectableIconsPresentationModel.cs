using System;
using System.Collections.Generic;
using Custom.Config;

namespace Custom.View.PresentationModel
{
    public interface ISelectableIconsPresentationModel : ISaveValuesPresentationModel
    {
        event Action<string> OnIconSelected;

        List<SpriteEntry> IconsEntries { get; }
        void SetIconSelected(string id);
        string GetCurrentIcon();
    }
}