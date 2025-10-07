using UnityEngine;

namespace Project.Runtime.Scripts
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Visuals")]
        public Material Material;
        public Sprite Icon;
        
        [Header("Keybindings")]
        public KeyCode MoveUp = KeyCode.W;
        public KeyCode MoveLeft = KeyCode.A;
        public KeyCode MoveDown = KeyCode.S;
        public KeyCode MoveRight = KeyCode.D;
        
        [Header("Button Icons")]
        public Sprite MoveUpIcon;
        public Sprite MoveUpIconPressed;
        public Sprite MoveLeftIcon;
        public Sprite MoveLeftIconPressed;
        public Sprite MoveDownIcon;
        public Sprite MoveDownIconPressed;
        public Sprite MoveRightIcon;
        public Sprite MoveRightIconPressed;
    }
}