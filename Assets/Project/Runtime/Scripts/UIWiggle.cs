using UnityEngine;

namespace Project.Runtime.Scripts.Effects
{
    public class UIWiggle : MonoBehaviour
    {
        [SerializeField] private float _amplitude = 10f;
        [SerializeField] private float _frequency = 5f;
        
        private RectTransform _rectTransform;
        private float _startTime;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startTime = Time.time;
        }

        private void Update()
        {
            float angle = Mathf.Sin((Time.time - _startTime) * _frequency * Mathf.PI * 2f) * _amplitude;
            _rectTransform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        private void OnDisable()
        {
            _rectTransform.localRotation = Quaternion.identity;
        }
    }
}