using System.Collections.Generic;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using Weapons.Bullets;


namespace StaticData.Data
{
    [CreateAssetMenu(menuName = "StaticData/BulletsUIConfig", fileName = "BulletsUIConfig")]
    public class BulletsUIConfig : SerializedScriptableObject
    {
        public Dictionary<AmmoType, BulletUI> bulletsUIConfig = new Dictionary<AmmoType, BulletUI>();
    }
}
