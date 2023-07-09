using System;
using UnityEngine;

namespace Views
{
    public class ProjectileView : EntityMono
    {
        private int _team0BulletLayer;
        private int _team1BulletLayer;
        private Vector3 _oldPosition;

        private void Awake()
        {
            _team0BulletLayer = LayerMask.NameToLayer("Team1Bullet");
            _team1BulletLayer = LayerMask.NameToLayer("Team2Bullet");
        }


        public override void SetTeam(int team)
        {
            base.SetTeam(team);
            _oldPosition = transform.position;
            if (team == 0)
            {
                var layer = _team0BulletLayer;
                SetLayer(layer);
            }

            if (team == 1)
            {
                var layer = _team1BulletLayer;
                SetLayer(layer);
            }
        }
    }
}