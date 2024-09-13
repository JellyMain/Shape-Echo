using UnityEngine;
using Weapons;


namespace StaticData.Data
{
    [CreateAssetMenu(menuName = "StaticData/PlayerStaticData", fileName = "PlayerStaticData")]
    public class PlayerStaticData : ScriptableObject
    {
        public WeaponBase defaultWeaponBase;
        public float weaponRotationSpeed = 10;
        
        public float rotationSpeed = 10;
        public float dashSpeed = 30;
        public float dashDuration = 1;
        public float moveSpeed = 20;
    }
}
