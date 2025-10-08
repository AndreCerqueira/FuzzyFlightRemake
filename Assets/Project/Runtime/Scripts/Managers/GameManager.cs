using System.Collections.Generic;
using Project.Runtime.Scripts.General;
using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Game Settings")]
        [SerializeField] private int _playerQuantity = 1;
        public int PlayerQuantityAlive { get; private set; }
        
        [Header("Containers")]
        [SerializeField] private Transform _shipContainer;
        [SerializeField] private Transform _hudContainer;
        
        [Header("Prefabs")]
        [SerializeField] private Ship _playerShipPrefab;
        [SerializeField] private PlayerHUD _playerHudPrefab;
        
        [Header("Player Data")]
        [SerializeField] private PlayerDataSO[] _playerDataSOs;
        
        [Header("Spawn Area (X Axis)")]
        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;
        [SerializeField] private float _spawnY = -2f;
        
        
        private void Start()
        {
            SpawnPlayers();
            PlayerQuantityAlive = _playerQuantity;
        }
        
        private void SpawnPlayers()
        {
            if (_playerQuantity <= 0) return;

            var totalWidth = _maxX - _minX;
            var spacing = _playerQuantity > 1 ? totalWidth / (_playerQuantity - 1) : 0f;
            var offset = (_maxX + _minX) / 2f;

            for (var i = 0; i < _playerQuantity; i++)
            {
                var x = _minX + spacing * i - offset;
                var data = _playerDataSOs[i];
                SpawnShip(x, _spawnY, data);
            }
        }

        private void SpawnShip(float x, float y, PlayerDataSO data)
        {
            var hud = Instantiate(_playerHudPrefab, _hudContainer);
            hud.Setup(data);

            var ship = Instantiate(_playerShipPrefab, new Vector3(x, y, 0f), Quaternion.identity, _shipContainer);
            ship.Setup(hud, data);
        }
        
        public void NotifyPlayerDeath()
        {
            if (PlayerQuantityAlive <= 0) return;
            PlayerQuantityAlive--;
        }

        public string GetCurrentPlayerPosition()
        {
            return PlayerQuantityAlive switch
            {
                3 => "4th",
                2 => "3rd",
                1 => "2nd",
                _ => "1st" 
            };
        }
    }
}
