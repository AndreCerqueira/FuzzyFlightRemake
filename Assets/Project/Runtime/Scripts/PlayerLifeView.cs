using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.Scripts
{
    public class PlayerLifeView : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            SetOn();
        }
        
        public void SetOn()
        {
            _image.color = Color.white;
        }
        
        public void SetOff()
        {
            _image.color = new Color(171f/255f, 171f/255f, 171f/255f, 159f/255f);
        }
    }
}
