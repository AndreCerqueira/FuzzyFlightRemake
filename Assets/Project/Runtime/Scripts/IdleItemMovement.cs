using UnityEngine;

namespace Project.Runtime.Scripts.UI
{
    public class IdleItemMovement : MonoBehaviour
    {
        private Camera _mainCamera;

        public enum MovementAxis
        {
            Horizontal,
            Vertical
        }

        [Header("Idle Movement Settings")]
        [SerializeField] private float _moveAmplitude = 0.5f;
        [SerializeField] private float _moveSpeed = 1.0f;
        [SerializeField] private MovementAxis _movementAxis = MovementAxis.Horizontal;

        private Vector3 _startPosition;

        private void Start()
        {
            _mainCamera = Camera.main;

            _startPosition = transform.localPosition;
        }

        private void Update()
        {
            if (_mainCamera == null)
                return;

            var cameraEuler = _mainCamera.transform.eulerAngles;
            transform.rotation = Quaternion.Euler(cameraEuler.x, transform.eulerAngles.y, transform.eulerAngles.z);

            float offset = Mathf.PingPong(Time.time * _moveSpeed, _moveAmplitude * 2) - _moveAmplitude;

            switch (_movementAxis)
            {
                case MovementAxis.Horizontal:
                    transform.localPosition = _startPosition + new Vector3(offset, 0, 0);
                    break;
                case MovementAxis.Vertical:
                    transform.localPosition = _startPosition + new Vector3(0, offset, 0);
                    break;
            }
        }
    }
}
