using System.Linq;
using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public enum EnemyType
        {
            Static,
            Vertical,
            Horizontal
        }

        [Header("Enemy Settings")]
        public EnemyType Type = EnemyType.Static;
        public float MoveDistance = 1.8f;
        public float MoveSpeed = 2f;
        public float DestroyScaleTime = 0.2f;

        private Vector3 _startLocalPos;
        private float _phaseOffset;
        private bool _isDestroying;

        [Header("References")]
        [SerializeField] private Transform _slider;
        [SerializeField] private MMF_Player _hitSfxFeedback;

        private void Start()
        {
            _startLocalPos = transform.localPosition;
            _phaseOffset = Random.Range(0f, Mathf.PI * 2f);

            if (Type != EnemyType.Static)
                _slider.gameObject.SetActive(true);
            if (Type == EnemyType.Vertical)
                _slider.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }

        private void Update()
        {
            MovePattern();

            if (transform.position.z < -1f && !_isDestroying)
            {
                _isDestroying = true;
                
                transform.parent.DOScale(Vector3.zero, DestroyScaleTime)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => Destroy(gameObject));
            }
        }

        private void MovePattern()
        {
            var offset = Mathf.Sin(Time.time * MoveSpeed + _phaseOffset) * MoveDistance;
            var pos = _startLocalPos;

            switch (Type)
            {
                case EnemyType.Vertical:
                    pos.y = _startLocalPos.y + offset;
                    break;
                case EnemyType.Horizontal:
                    pos.x = _startLocalPos.x + offset;
                    break;
            }

            transform.localPosition = pos;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Ship")) return;

            var ship = other.GetComponentInParent<Ship>();
            if (ship != null)
            {
                ship.TakeDamage();
            }
            
            _hitSfxFeedback?.PlayFeedbacks();
            
            Destroy(transform.parent.gameObject);
        }
    }
}
