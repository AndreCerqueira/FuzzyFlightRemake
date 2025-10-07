using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField] private int _playerQuantity = 1;
        
        [Header("Containers")]
        [SerializeField] private Transform _shipContainer;
        [SerializeField] private Transform _hudContainer;
        
        [Header("Prefabs")]
        [SerializeField] private Ship _playerShipPrefab;
        [SerializeField] private PlayerHUD _playerHudPrefab;
        
        
        private void Start()
        {
            for (var i = 0; i < _playerQuantity; i++)
            {
                var hud = Instantiate(_playerHudPrefab, _hudContainer);
                var ship = Instantiate(_playerShipPrefab, _shipContainer);
                ship.Setup(hud);
            }
        }
    }
}
