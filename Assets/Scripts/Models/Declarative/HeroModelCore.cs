using System;

namespace Models.Declarative
{
    public class HeroModelCore : IDisposable
    {
        public readonly Life Life = new Life();
        public readonly MoveModel MoveModel = new MoveModel();
        public readonly AttackModel AttackModel = new AttackModel();

        public void Construct()
        {
            Life.Construct();
            MoveModel.Construct();
            AttackModel.Construct();
        }

        public void Dispose()
        {
            Life.Dispose();
            MoveModel.Dispose();
            AttackModel.Dispose();
        }
    }
}