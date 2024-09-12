using Sirenix.OdinInspector;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField, Required] private Rigidbody2D rb2d;
    public Rigidbody2D Rb2d => rb2d;
    
}
