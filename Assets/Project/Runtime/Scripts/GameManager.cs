using System.Collections.Generic;
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
        
        [Header("Player Data")]
        [SerializeField] private PlayerDataSO[] _playerDataSOs;
        private List<int> _availableDataIndices;
        
        [Header("Spawn Area (X Axis)")]
        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;
        [SerializeField] private float _spawnY = -2f;
        
        
        private void Start()
        {
            _availableDataIndices = new List<int>();
            for (var i = 0; i < _playerDataSOs.Length; i++)
                _availableDataIndices.Add(i);
            
            SpawnPlayers();
        }
        
        private void SpawnPlayers()
        {
            if (_playerQuantity <= 0) return;

            if (_playerQuantity == 1)
            {
                SpawnShip(0f, _spawnY);
                return;
            }

            var totalWidth = _maxX - _minX;
            var spacing = totalWidth / (_playerQuantity - 1);
            var offset = (_maxX + _minX) / 2f;

            for (var i = 0; i < _playerQuantity; i++)
            {
                var x = _minX + spacing * i - offset;
                SpawnShip(x, _spawnY);
            }
        }

        private void SpawnShip(float x, float y)
        {
            if (_availableDataIndices.Count == 0) return;

            var data = GetPlayerData();
            
            var hud = Instantiate(_playerHudPrefab, _hudContainer);
            var ship = Instantiate(_playerShipPrefab, new Vector3(x, y, 0f), Quaternion.identity, _shipContainer);
            
            hud.Setup(data.Icon);
            ship.Setup(hud, data);
        }

        private PlayerDataSO GetPlayerData()
        {
            var randomIndex = Random.Range(0, _availableDataIndices.Count);
            var dataIndex = _availableDataIndices[randomIndex];
            _availableDataIndices.RemoveAt(randomIndex);

            return _playerDataSOs[dataIndex];
        }
    }
}
