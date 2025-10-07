using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class Enemy : MonoBehaviour
    {
        
        
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Enemy hit something!");
            if (other.CompareTag("Ship"))
            {
                other.GetComponentInParent<Ship>().TakeDamage();
            }
        }
        
    }
}
