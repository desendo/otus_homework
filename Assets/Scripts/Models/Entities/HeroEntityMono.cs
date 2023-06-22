using Common.Entities;
using Config;
using DependencyInjection;
using GameManager;
using Models.Components;
using Models.Declarative;
using UnityEngine;

namespace Models.Entities
{
    public class HeroEntityMono : EntityMono
    {
        private readonly HeroModel _heroModel = new HeroModel();
        private IUpdateProvider _updateProvider;

        [Inject]
        public void Construct(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
        }

        private void Awake()
        {
            var animator = GetComponentInChildren<Animator>();
            _heroModel.Construct(transform, animator, _updateProvider);

            Add(new Component_Move(_heroModel.Core.MoveModel.OnMoveDir, _heroModel.Core.MoveModel.MoveRequested, _heroModel.Core.MoveModel.ResultVelocity));
            Add(new Component_Transform(_heroModel.Visual.RootTransform));
            Add(new Component_Rotate(_heroModel.Core.MoveModel.RotationSpeed));

        }

        public void Setup(object parameters)
        {
            if (parameters is GameConfig config)
            {
                _heroModel.Core.MoveModel.Speed.Value = config.PlayerMoveSpeed;
                _heroModel.Core.MoveModel.RotationSpeed.Value = config.PlayerRotationSpeed;
            }
        }
    }
}