using DependencyInjection;
using Homework.Data;
using Homeworks.SaveLoad;

namespace Homework.SceneObjects
{
    public class ResourceObjectHandler : SceneObjectsHandler<SceneObjectView, ResourceObjectData, ResourceObject>
    {
        [Inject]
        public void Construct(SceneObjectPool pool)
        {
            _pool = pool;
        }
        protected override void InitializeElementData(ResourceObjectData dataElement, ResourceObject unitObject)
        {
            dataElement.RemainingCount = unitObject.remainingCount;
            dataElement.ResourceType = unitObject.resourceType;
        }

        protected override void InitializeObject(ResourceObjectData dataElement, ResourceObject unitObject)
        {
            unitObject.remainingCount = dataElement.RemainingCount;
            unitObject.resourceType = dataElement.ResourceType;
        }
    }
}