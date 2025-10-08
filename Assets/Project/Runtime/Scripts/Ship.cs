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
            _playerHUD.OnPlayerDead += () => GameManager.Instance.NotifyPlayerDeath();
        }
        
        public void TakeDamage()
        {
            if (_shipMovement == null) return;
            if (DOTween.IsTweening(transform)) return;
            
            _playerHUD.TakeDamage();
            
            if (_playerHUD.Lives > 0)
                PlayHitAnimation();
        }
        
        private void PlayHitAnimation()
        {
            if (_shipMovement == null) return;

            _shipMovement.enabled = false;

            var startRot = transform.rotation;
            var startPos = transform.position;
            var hitBackPos = startPos - transform.forward * 0.7f;
            var hitSeq = DOTween.Sequence();

            hitSeq.Append(
                transform.DOLocalRotate(new Vector3(0f, 360f, 0f), 0.6f, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutCubic)
            );

            hitSeq.Join(
                transform.DOMove(hitBackPos, 0.3f)
                    .SetEase(Ease.OutCubic)
            );

            hitSeq.Append(
                transform.DOMove(startPos, 0.3f)
                    .SetEase(Ease.InOutCubic)
            );

            hitSeq.OnComplete(() =>
            {
                transform.rotation = startRot;
                _shipMovement.enabled = true;
            });
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
