using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class Ship : MonoBehaviour
    {
        private ShipMovement _shipMovement;
        private PlayerHUD _playerHUD;

        public void Setup(PlayerHUD playerHUD)
        {
            _shipMovement = GetComponent<ShipMovement>();
            _playerHUD = playerHUD;
        }
        
        public void TakeDamage()
        {
            Debug.Log($"Player took 1 damage!");
            _playerHUD.TakeDamage();
        }
    }
}
