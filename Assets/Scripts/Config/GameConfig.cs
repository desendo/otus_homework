using Bullets;
using UnityEngine;
using UnityEngine.Serialization;

namespace Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config/New Game Config")]
    public sealed class GameConfig : ScriptableObject
    {
        [SerializeField] public float CharMoveSpeed;
        [SerializeField] public int CharHealth;
        [SerializeField] public float EnemySpawnInterval;
        [SerializeField] public int EnemyHealth;
        [SerializeField] public float EnemyMoveSpeed;
        [SerializeField] public float EnemyFireInterval;
        [SerializeField] public float EnemyPositionRandomRadius = 1;
        [SerializeField] public bool EnemyFireImmediately;
        [SerializeField] public BulletConfig EnemyBulletConfig;
        [SerializeField] public BulletConfig PlayerBulletConfig;
    }
}