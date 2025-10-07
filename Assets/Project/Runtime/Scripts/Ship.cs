using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        
        private ShipMovement _shipMovement;
        private PlayerHUD _playerHUD;

        public void Setup(PlayerHUD playerHUD, PlayerDataSO data)
        {
            _shipMovement = GetComponent<ShipMovement>();
            _playerHUD = playerHUD;
            
            if (_renderer != null && data.Material != null)
                _renderer.material = new Material(data.Material);
        }
        
        public void TakeDamage()
        {
            Debug.Log($"Player took 1 damage!");
            _playerHUD.TakeDamage();
        }
    }
}
