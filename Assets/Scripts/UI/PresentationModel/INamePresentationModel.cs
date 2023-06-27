using UI.PresentationModel;

namespace Custom.View.PresentationModel
{
    public interface INamePresentationModel: ISaveValuesPresentationModel
    {
        string GetName();
        void SetName(string name);
        void SetTemporaryName(string name);

    }
}