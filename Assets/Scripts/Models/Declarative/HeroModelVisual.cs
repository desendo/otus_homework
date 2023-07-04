using System;
using System.Collections.Generic;
using Common;
using GameManager;
using UnityEngine;

namespace Models.Declarative
{
    public class HeroModelVisual
    {
        public Transform RootTransform;

        private static readonly int state = Animator.StringToHash("state");
        private static readonly int x = Animator.StringToHash("move_speed_x");
        private static readonly int y = Animator.StringToHash("move_speed_y");
        private static readonly int shot = Animator.StringToHash("shot");
        private static readonly int reload = Animator.StringToHash("reload");
        private static readonly int reload_animation_speed = Animator.StringToHash("reload_animation_speed");

        private readonly List<IDisposable> _subs = new List<IDisposable>();
        private const int IDLE_STATE = 0;
        private const int MOVE_STATE = 1;
        private const int DEATH_STATE = 5;

        private Animator _animator;
        private float _reloadAnimationTime;
        public Rigidbody Rigidbody { get; private set; }


        public void Construct(HeroModelCore core, Transform rootTransform, Animator animator, IUpdateProvider updateProvider, Rigidbody rigidbody)
        {
            _animator = animator;
            var clips = _animator.runtimeAnimatorController.animationClips;
            foreach (var animationClip in clips)
            {
                if (animationClip.name.Contains("Reload"))
                    _reloadAnimationTime = animationClip.length;
            }

            Rigidbody = rigidbody;
            RootTransform = rootTransform;
            var isDead = core.LifeModel.IsDead;
            core.AttackModel.OnReloadStarted.Subscribe(reloadTime =>
            {
                var speed =  _reloadAnimationTime / reloadTime;
                _animator.SetFloat(reload_animation_speed, speed);
                _animator.ResetTrigger(reload);
                _animator.SetTrigger(reload);
            }).AddTo(_subs);
            core.AttackModel.OnAttackStart.Subscribe(() =>
            {
                _animator.ResetTrigger(shot);
                _animator.SetTrigger(shot);
            }).AddTo(_subs);
            updateProvider.OnLateUpdate.Subscribe(() => {
                if (isDead.Value)
                {
                    _animator.SetInteger(state, DEATH_STATE);
                    return;
                }

                if (core.MoveModel.IsMoving.Value)
                {
                    _animator.SetInteger(state, MOVE_STATE);
                    var velocity = core.MoveModel.ResultVelocity.Value.normalized;

                    var translatedVelocity = new Vector2(Vector3.Dot(velocity, rootTransform.right),
                        Vector3.Dot(velocity, rootTransform.forward));

                    _animator.SetFloat(x, translatedVelocity.x);
                    _animator.SetFloat(y, translatedVelocity.y);
                }
                else
                {
                    _animator.SetInteger(state, IDLE_STATE);
                }
            }).AddTo(_subs);
        }

        public void Dispose()
        {
            _subs.Dispose();
        }
    }
}