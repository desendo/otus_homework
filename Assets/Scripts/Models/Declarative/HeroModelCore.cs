namespace Models.Declarative
{
    public class HeroModelCore
    {
        public readonly LifeModel LifeModel = new LifeModel();
        public readonly MoveModel MoveModel = new MoveModel();

        public void Construct()
        {
            LifeModel.Construct();
            MoveModel.Construct();
        }
    }
}