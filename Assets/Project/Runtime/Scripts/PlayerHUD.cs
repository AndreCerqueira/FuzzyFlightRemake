using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Runtime.Scripts
{
    public class PlayerHUD : MonoBehaviour
    {
        public event Action OnPlayerDead;
        
        [SerializeField] private Image _playerHudImage;
        [SerializeField] private PlayerLifeView[] _lifeViews;

        [SerializeField] private Image _upKeyIcon;
        [SerializeField] private Image _leftKeyIcon;
        [SerializeField] private Image _downKeyIcon;
        [SerializeField] private Image _rightKeyIcon;

        private Sprite _upNormal, _upPressed;
        private Sprite _leftNormal, _leftPressed;
        private Sprite _downNormal, _downPressed;
        private Sprite _rightNormal, _rightPressed;
        
        private PlayerDataSO _playerData;

        public int Lives { get; private set; } = 3;
        
        private bool _upPressedOnce = false;
        private bool _leftPressedOnce = false;
        private bool _downPressedOnce = false;
        private bool _rightPressedOnce = false;

        public void Setup(PlayerDataSO data)
        {
            _playerData = data;

            if (_playerHudImage != null)
                _playerHudImage.sprite = data.Icon;

            if (_upKeyIcon != null)
            {
                _upKeyIcon.sprite = data.MoveUpIcon;
                _upNormal = data.MoveUpIcon;
                _upPressed = data.MoveUpIconPressed;
            }

            if (_leftKeyIcon != null)
            {
                _leftKeyIcon.sprite = data.MoveLeftIcon;
                _leftNormal = data.MoveLeftIcon;
                _leftPressed = data.MoveLeftIconPressed;
            }

            if (_downKeyIcon != null)
            {
                _downKeyIcon.sprite = data.MoveDownIcon;
                _downNormal = data.MoveDownIcon;
                _downPressed = data.MoveDownIconPressed;
            }

            if (_rightKeyIcon != null)
            {
                _rightKeyIcon.sprite = data.MoveRightIcon;
                _rightNormal = data.MoveRightIcon;
                _rightPressed = data.MoveRightIconPressed;
            }
        }

        private void Update()
        {
            if (_playerData == null) return;

            if (_upKeyIcon != null)
                _upKeyIcon.sprite = Input.GetKey(_playerData.MoveUp) ? _upPressed : _upNormal;

            if (_leftKeyIcon != null)
                _leftKeyIcon.sprite = Input.GetKey(_playerData.MoveLeft) ? _leftPressed : _leftNormal;

            if (_downKeyIcon != null)
                _downKeyIcon.sprite = Input.GetKey(_playerData.MoveDown) ? _downPressed : _downNormal;

            if (_rightKeyIcon != null)
                _rightKeyIcon.sprite = Input.GetKey(_playerData.MoveRight) ? _rightPressed : _rightNormal;
        }
        
        public void TakeDamage()
        {
            if (Lives <= 0) return;
            
            Lives--;
            _lifeViews[Lives].SetOff();

            if (Lives == 0)
            {
                Debug.Log("Player is dead!");
                OnPlayerDead?.Invoke();
            }
        }
        
        /*
         *versao com flags
         *
         
         
        private void Update()
        {
            if (_playerData == null) return;

            if (_upKeyIcon != null)
            {
                var pressed = Input.GetKey(_playerData.MoveUp);
                _upKeyIcon.sprite = pressed ? _upPressed : _upNormal;
                if (pressed) _upPressedOnce = true;
            }

            if (_leftKeyIcon != null)
            {
                var pressed = Input.GetKey(_playerData.MoveLeft);
                _leftKeyIcon.sprite = pressed ? _leftPressed : _leftNormal;
                if (pressed) _leftPressedOnce = true;
            }

            if (_downKeyIcon != null)
            {
                var pressed = Input.GetKey(_playerData.MoveDown);
                _downKeyIcon.sprite = pressed ? _downPressed : _downNormal;
                if (pressed) _downPressedOnce = true;
            }

            if (_rightKeyIcon != null)
            {
                var pressed = Input.GetKey(_playerData.MoveRight);
                _rightKeyIcon.sprite = pressed ? _rightPressed : _rightNormal;
                if (pressed) _rightPressedOnce = true;
            }

            if (_upPressedOnce && _leftPressedOnce && _downPressedOnce && _rightPressedOnce)
            {
                if (_upKeyIcon != null) _upKeyIcon.gameObject.SetActive(false);
                if (_leftKeyIcon != null) _leftKeyIcon.gameObject.SetActive(false);
                if (_downKeyIcon != null) _downKeyIcon.gameObject.SetActive(false);
                if (_rightKeyIcon != null) _rightKeyIcon.gameObject.SetActive(false);
            }
        }
         
         */
    }
}
