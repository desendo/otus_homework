using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private HitPointsComponent  _hitPointsComponent;
        [SerializeField] private EnemyMoveAgent  _enemyMoveAgent;
        [SerializeField] private WeaponComponent  _weaponComponent;
        [SerializeField] private EnemyAttackAgent  _enemyAttackAgent;
        [SerializeField] private TeamComponent  _teamComponent;

        public TeamComponent TeamComponent => _teamComponent;

        public EnemyAttackAgent EnemyAttackAgent => _enemyAttackAgent;

        public WeaponComponent WeaponComponent => _weaponComponent;

        public EnemyMoveAgent EnemyMoveAgent => _enemyMoveAgent;

        public HitPointsComponent HitPointsComponent => _hitPointsComponent;

        public MoveComponent MoveComponent => _moveComponent;
    }
}
