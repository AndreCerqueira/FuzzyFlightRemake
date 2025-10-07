using UnityEngine;

namespace Project.Runtime.Scripts
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Visuals")]
        public Material Material;
        public Sprite Icon;
    }
}