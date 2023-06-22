using GameManager;
using UnityEngine;

namespace Models.Declarative
{
    public class HeroModelVisual
    {
        public Transform RootTransform;

        private static readonly int State = Animator.StringToHash("State");
        private const int IDLE_STATE = 0;
        private const int MOVE_STATE = 1;
        private const int DEATH_STATE = 5;

        private Animator _animator;
        private IUpdateProvider _updateProvider;


        public void Construct(HeroModelCore core, Transform rootTransform, Animator animator, IUpdateProvider updateProvider)
        {
            _animator = animator;
            RootTransform = rootTransform;
            _updateProvider = updateProvider;
            var isDead = core.LifeModel.IsDead;
            updateProvider.OnLateUpdate += () => {
                if (isDead.Value)
                {
                    _animator.SetInteger(State, DEATH_STATE);
                    return;
                }

                if (core.MoveModel.IsMoving.Value)
                {
                    _animator.SetInteger(State, MOVE_STATE);
                    var velocity = core.MoveModel.ResultVelocity.Value;
                    var relativeRotation = Vector3.SignedAngle(rootTransform.forward, velocity, rootTransform.up);
                    Debug.Log(relativeRotation);
                }
                else
                {
                    _animator.SetInteger(State, IDLE_STATE);
                }
            };

        }

    }
}