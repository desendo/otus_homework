namespace Homework.Data
{
    public class ResourceObjectData : ViewDataElement, IViewData
    {
        public ResourceType ResourceType;
        public int RemainingCount;
        public ViewData ViewData;
    }
}