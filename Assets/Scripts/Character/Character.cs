using Components;
using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private TeamComponent _teamComponent;
        private Vector3 _initialPosition;
        public HitPointsComponent HitPointsComponent => _hitPointsComponent;
        public MoveComponent MoveComponent => _moveComponent;
        public TeamComponent TeamComponent => _teamComponent;
        public WeaponComponent WeaponComponent => _weaponComponent;

        private void Awake()
        {
            _initialPosition = transform.position;
        }

        public void DoReset()
        {
            transform.position = _initialPosition;
            _hitPointsComponent.DoReset();
        }
    }
}