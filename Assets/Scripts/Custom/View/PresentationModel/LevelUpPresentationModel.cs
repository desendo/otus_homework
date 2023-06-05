namespace Custom.View.PresentationModel
{
    public interface ILevelUpPresentationModel
    {
        IExperiencePresentationModel ExperiencePresentationModel { get; }
        IUserInfoPresentationModel UserInfoPresentationModel { get; }
        ICharacterInfoPresentationModel CharacterInfoPresentationModel { get; }
    }
    public class LevelUpPresentationModel : ILevelUpPresentationModel
    {
        public IExperiencePresentationModel ExperiencePresentationModel { get; }
        public IUserInfoPresentationModel UserInfoPresentationModel { get; }
        public ICharacterInfoPresentationModel CharacterInfoPresentationModel { get; }

        public LevelUpPresentationModel(IUserInfoPresentationModel userInfoPresentationModel, PlayerLevel playerLevel, CharacterInfo characterInfo)
        {
            //эта PM используется двумя разными окошками, поэтому мы ее биндим в DI контейнере и вытаскиваем уже готовую напрямую
            UserInfoPresentationModel = userInfoPresentationModel;

            //За пределами LevelUpPresentationModel мы пока не используем эти PM, поэтому храним и инициализируем тут.
            //Например, вдруг у нас будет несколько персонажей.
            ExperiencePresentationModel = new ExperiencePresentationModel(playerLevel);
            CharacterInfoPresentationModel = new CharacterInfoPresentationModel(characterInfo);
        }
    }
}
