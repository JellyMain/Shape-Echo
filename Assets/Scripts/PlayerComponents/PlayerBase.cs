using Sirenix.OdinInspector;
using UnityEngine;


namespace PlayerComponents
{
    public class PlayerBase : MonoBehaviour
    {
        [Required] public PlayerHealth playerHealth;
        [Required] public PlayerShooting playerShooting;
        [Required] public PlayerMovement playerMovement;
    }
}
