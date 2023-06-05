using UnityEngine;

namespace Custom.Data
{
    public abstract class DataContainer<T> : ScriptableObject where T : class
    {
        public T Data;
    }
}