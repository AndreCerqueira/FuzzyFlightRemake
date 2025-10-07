using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private PlayerLifeView[] _lifeViews;

        private int _lives = 3;
        
        public void TakeDamage()
        {
            if (_lives <= 0) return;
            
            _lives--;
            _lifeViews[_lives].SetOff();

            if (_lives == 0)
            {
                Debug.Log("Player is dead!");
            }
        }
    }
}
