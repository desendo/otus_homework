using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner
{
    private readonly List<WorldObjectBase> _list;

    public ObjectSpawner(List<WorldObjectBase> list)
    {
        _list = list;
    }

    public WorldObjectBase Spawn(WorldObjectBase prefab, Vector3 position)
    {
        var obj = Object.Instantiate(prefab, position, Quaternion.identity);
        obj.OnObjectDestroy += OnObjectDestroy;
        _list.Add(obj);
        return obj;
    }

    private void OnObjectDestroy(WorldObjectBase obj)
    {
        obj.OnObjectDestroy -= OnObjectDestroy;
        _list.Remove(obj);
    }
}