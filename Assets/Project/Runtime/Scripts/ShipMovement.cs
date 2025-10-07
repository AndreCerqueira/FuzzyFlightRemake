using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class ShipMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float MoveSpeed = 7f;
        public float Smoothness = 8f;

        [Header("Tilt Settings")]
        public float MaxTiltX = 25f; // inclinação vertical
        public float MaxTiltZ = 15f; // inclinação horizontal
        public float TiltSmoothness = 5f;

        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        private void Start()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            HandleMovement();
            HandleTilt();
        }

        private void HandleMovement()
        {
            var inputX = Input.GetAxis("Horizontal");
            var inputY = Input.GetAxis("Vertical");

            var direction = new Vector3(inputX, inputY, 0f);
            _targetPosition += direction * MoveSpeed * Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * Smoothness);
        }

        private void HandleTilt()
        {
            var inputX = Input.GetAxis("Horizontal");
            var inputY = Input.GetAxis("Vertical");

            if (Mathf.Abs(inputX) < 0.05f && Mathf.Abs(inputY) < 0.05f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * TiltSmoothness);
                return;
            }

            var tiltX = -inputY * MaxTiltX;
            var tiltZ = -inputX * MaxTiltZ;

            var desiredRot = Quaternion.Euler(tiltX, 0f, tiltZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, Time.deltaTime * TiltSmoothness);
        }
    }
}
