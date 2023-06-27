using System;

namespace Models.Declarative
{
    public class HeroModelCore : IDisposable
    {
        public readonly LifeModel LifeModel = new LifeModel();
        public readonly MoveModel MoveModel = new MoveModel();
        public readonly AttackModel AttackModel = new AttackModel();

        public void Construct()
        {
            LifeModel.Construct();
            MoveModel.Construct();
            AttackModel.Construct();
        }

        public void Dispose()
        {
            LifeModel.Dispose();
            MoveModel.Dispose();
            AttackModel.Dispose();
        }
    }
}