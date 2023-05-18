using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 _velocity;
    private Action<Enemy> _onEnemyDestroy;



    public void Initialize(Vector3 velocity, Action<Enemy> onEnemyDestroy)
    {
        _onEnemyDestroy = onEnemyDestroy;
        _velocity = velocity;
    }

    private void OnDestroy()
    {
        _onEnemyDestroy.Invoke(this);
    }

    public void Move(float dt)
    {
        transform.position += _velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject.Destroy(gameObject);
    }
}