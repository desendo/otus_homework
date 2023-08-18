using Common.Entities;
using Config;
using GameManager;
using UnityEngine;

namespace Models.Entities
{
    public abstract class WeaponEntityBase : Entity, IWeapon
    {
        protected readonly IUpdateProvider UpdateProvider;
        protected readonly Transform ShootTransform;

        public WeaponType WeaponType { get; }
        public string Id { get; }

        protected WeaponEntityBase(IUpdateProvider updateProvider, Transform shootTransform, WeaponType weaponType, string id)
        {
            Id = id;
            WeaponType = weaponType;
            ShootTransform = shootTransform;
            UpdateProvider = updateProvider;
        }


    }
}