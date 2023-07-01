using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DependencyInjection.Util
{
    public static class InstantiateUtil
    {
        public static T Instantiate<T>(T unityObject,  Transform parent = null, Action<T> beforeAwake = null) where T : Object
        {
            var gameObject = unityObject as GameObject;
            var component = unityObject as Component;

            if (gameObject == null && component != null)
                gameObject = component.gameObject;

            var savedActiveState = false;
            if (gameObject != null)
            {
                savedActiveState = gameObject.activeSelf;
                gameObject.SetActive(false);
            }

            var obj = Object.Instantiate(unityObject, parent);
            if (obj == null)
                throw new Exception("Failed to instantiate Object " + unityObject);

            beforeAwake?.Invoke(obj);

            if (gameObject != null)
                gameObject.SetActive(savedActiveState);

            gameObject = obj as GameObject;
            component = obj as Component;

            if (gameObject == null && component != null)
                gameObject = component.gameObject;

            if (gameObject != null)
                gameObject.SetActive(savedActiveState);

            return obj;
        }
    }
}