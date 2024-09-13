using Sirenix.OdinInspector;
using UnityEngine;


namespace Weapons.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Required] private Rigidbody2D rb2d;
        public Rigidbody2D Rb2d => rb2d;
    
    }
}
