using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Scripts.VictoryScreen
{
    public class GameResultManager : MonoBehaviour
    {
        public static GameResultManager Instance { get; private set; }

        // Armazena info por jogador
        [System.Serializable]
        public class PlayerResult
        {
            public string playerName;
            public bool survived;
        }

        public List<PlayerResult> Results = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void ClearResults()
        {
            Results.Clear();
        }

        public void AddResult(string playerName, bool survived)
        {
            Results.Add(new PlayerResult
            {
                playerName = playerName,
                survived = survived,
            });
        }
    }
}