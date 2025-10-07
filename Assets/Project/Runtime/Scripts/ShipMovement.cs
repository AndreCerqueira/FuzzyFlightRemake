using DG.Tweening;
using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class ShipMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float MoveSpeed = 7f;

        [Header("Tilt Settings")]
        public float MaxTiltX = 45f;
        public float MaxTiltZ = 30f;
        public float TiltSmoothness = 5f;

        [Header("Idle Settings")]
        public Transform VisualModel;
        public float IdleAmplitude = 0.25f;
        public float IdleSpeed = 1f;
        
        private Vector3 _input;
        private PlayerDataSO _playerData;
        private Rigidbody _rb;
        private Quaternion _startRotation;
        private Tween _idleTween;
        private bool _isIdle;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _startRotation = transform.rotation;
            
            if (VisualModel != null)
            {
                _idleTween = VisualModel.DOLocalMoveY(VisualModel.localPosition.y + IdleAmplitude, IdleSpeed)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            }
        }

        public void Setup(PlayerDataSO data)
        {
            _playerData = data;
        }

        private void Update()
        {
            if (_playerData == null) return;

            _input = Vector3.zero;
            if (Input.GetKey(_playerData.MoveUp)) _input.y += 1f;
            if (Input.GetKey(_playerData.MoveDown)) _input.y -= 1f;
            if (Input.GetKey(_playerData.MoveRight)) _input.x += 1f;
            if (Input.GetKey(_playerData.MoveLeft)) _input.x -= 1f;
            _input = _input.normalized;

            HandleTilt();
            HandleIdle();
        }

        private void FixedUpdate()
        {
            if (_playerData == null) return;

            var newPos = _rb.position + _input * MoveSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(newPos);
        }

        private void HandleTilt()
        {
            if (_input.magnitude < 0.05f)
            {
                _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, _startRotation, Time.deltaTime * TiltSmoothness));
                return;
            }

            var tiltX = -_input.y * MaxTiltX;
            var tiltZ = -_input.x * MaxTiltZ;
            
            var targetRot = _startRotation * Quaternion.Euler(tiltX, 0f, tiltZ);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRot, Time.deltaTime * TiltSmoothness));
        }
        
        private void HandleIdle()
        {
            if (VisualModel == null) return;

            bool shouldIdle = _input.magnitude < 0.05f;

            if (shouldIdle && !_isIdle)
            {
                _isIdle = true;
                StartIdleTween();
            }
            else if (!shouldIdle && _isIdle)
            {
                _isIdle = false;
                StopIdleTween();
            }
        }

        private void StartIdleTween()
        {
            if (_idleTween != null) _idleTween.Kill();
            _idleTween = VisualModel
                .DOLocalMoveY(IdleAmplitude, IdleSpeed)
                .SetRelative()
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void StopIdleTween()
        {
            if (_idleTween != null)
            {
                _idleTween.Kill();
                _idleTween = null;
            }

            VisualModel.DOLocalMoveY(0f, 0.3f).SetEase(Ease.OutSine);
        }
    }
}
