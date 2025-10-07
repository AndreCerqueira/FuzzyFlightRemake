using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class ShipMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float Speed = 5f;

        private float _inputY;
        private float _inputX;

        private void Update()
        {
            _inputY = Input.GetAxis("Vertical");
            _inputX = Input.GetAxis("Horizontal");

            var direction = new Vector3(_inputX, _inputY, 0f).normalized;

            transform.Translate(direction * Speed * Time.deltaTime, Space.World);
        }
    }
}
