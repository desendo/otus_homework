using Common.Entities;
using Config;

namespace Models.Entities
{
    public interface IWeapon : IEntity
    {
        WeaponType WeaponType { get;}
        string Id { get;}
    }
}