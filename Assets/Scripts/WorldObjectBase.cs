using System;
using UnityEngine;

public class WorldObjectBase : MonoBehaviour
{
    public enum ObjType
    {
        Enemy = 0,
        Coin = 1
    }

    [SerializeField] private ObjType _objType;
    private Vector3 _velocity;

    public virtual ObjType Type => _objType;
    public event Action<WorldObjectBase> OnObjectDestroy;
    public void Initialize(Vector3 velocity)
    {
        _velocity = velocity;
    }

    private void OnDestroy()
    {
        OnObjectDestroy?.Invoke(this);
    }

    public virtual void DoUpdate(float dt)
    {
        Move(dt);
    }

    private void Move(float dt)
    {
        transform.position += _velocity * dt;
    }

    private void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}