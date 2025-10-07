using DG.Tweening;
using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _deathSpinDuration = 1f;
        [SerializeField] private float _deathMoveBackDistance = 4f;
        
        private ShipMovement _shipMovement;
        private PlayerHUD _playerHUD;

        public void Setup(PlayerHUD playerHUD, PlayerDataSO data)
        {
            _shipMovement = GetComponent<ShipMovement>();
            _playerHUD = playerHUD;
            
            _shipMovement.Setup(data);
            
            if (_renderer != null && data.Material != null)
                _renderer.material = new Material(data.Material);
            
            _playerHUD.OnPlayerDead += HandleDeath;
        }
        
        public void TakeDamage()
        {
            Debug.Log($"Player took 1 damage!");
            _playerHUD.TakeDamage();
        }
        
        private void HandleDeath()
        {
            _shipMovement.enabled = false;
            
            var moveBack = -transform.forward * _deathMoveBackDistance;
            var deathSeq = DOTween.Sequence();
            deathSeq.Append(transform.DOLocalRotate(new Vector3(0f, 0f, 360f), _deathSpinDuration,
                    RotateMode.FastBeyond360))
                .Join(transform.DOMove(transform.position + moveBack, _deathSpinDuration));
        }
    }
}
