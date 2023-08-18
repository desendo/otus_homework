﻿using Common.Entities;
using Models.Components;
using Services;
using UnityEngine;

namespace Controllers
{
    public class EnemyMoveController : IFixedUpdate, IStartGameListener, IFinishGameListener
    {
        private readonly EnemyService _enemyService;
        private bool _gameStarted;
        private readonly WeaponManager _weaponManager;
        private readonly HeroService _heroService;

        public EnemyMoveController(EnemyService enemyService, WeaponManager weaponManager, HeroService heroService)
        {
            _heroService = heroService;
            _weaponManager = weaponManager;
            _enemyService = enemyService;
        }


        public void FixedUpdate(float dt)
        {
            if(!_gameStarted)
                return;

            var heroPos = _heroService.HeroEntity.Value.Get<Component_Transform>().RootTransform.position;
            foreach (var enemy in _enemyService.Units)
            {
                var isActive = enemy.Get<Component_IsActive>().IsActive.Value;
                if(!isActive)
                    continue;

                var root = enemy.Get<Component_Transform>().RootTransform;
                var enemyPos = root.position;
                var dir = (heroPos - enemyPos).normalized;
                root.forward = dir;
                var deltaMove = dir * (enemy.Get<Component_Speed>().Speed);
                enemy.Get<Component_Rigidbody>().SetVelocity(deltaMove);
            }
        }

        public void OnStartGame()
        {
            _gameStarted = true;
        }
        public void OnFinishGame(bool gameWin)
        {
            _gameStarted = false;
        }
    }
}