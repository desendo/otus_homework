namespace Models.Declarative
{
    public class ZombieModel
    {
        public ZombieModelCore Core = new ZombieModelCore();
        public ZombieModelVisual Visual = new ZombieModelVisual();

        public void Construct()
        {
        }
    }

    public class ZombieModelVisual
    {
    }

    public class ZombieModelCore
    {
    }
}