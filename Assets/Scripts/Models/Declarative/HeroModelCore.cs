using System;

namespace Models.Declarative
{
    public class HeroModelCore : IDisposable
    {
        public readonly Life Life = new Life();
        public readonly MoveModel MoveModel = new MoveModel();
        public readonly AttackModel AttackModel = new AttackModel();
        public readonly HeroEffectModel HeroEffectModel = new HeroEffectModel();

        public void Construct()
        {
            Life.Construct();
            MoveModel.Construct();
            AttackModel.Construct();
            HeroEffectModel.Construct(Life, MoveModel);
        }

        public void Dispose()
        {
            Life.Dispose();
            MoveModel.Dispose();
            AttackModel.Dispose();
        }
    }

}