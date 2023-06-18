using DependencyInjection;
using Homework.Data;
using Homeworks.SaveLoad;
using Pool;

namespace Homework.SceneObjects
{
    public class UnitObjectsHandler : SceneObjectsHandler<SceneObjectView, UnitData, UnitObject>
    {
        [Inject]
        public void Construct(SceneObjectPool pool)
        {
            _pool = pool;
        }
        protected override void InitializeElementData(UnitData dataElement, UnitObject unitObject)
        {
            dataElement.Damage = unitObject.damage;
            dataElement.Speed = unitObject.speed;
            dataElement.HitPoints = unitObject.hitPoints;
        }

        protected override void InitializeObject(UnitData dataElement, UnitObject unitObject)
        {
            unitObject.damage = dataElement.Damage;
            unitObject.speed = dataElement.Speed;
            unitObject.hitPoints = dataElement.HitPoints;
        }
    }
}