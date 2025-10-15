using System;
using MoreMountains.Feedbacks;
using Project.Runtime.Scripts.VictoryScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenManager : MonoBehaviour
{
    [Header("Players & Tags")]
    [SerializeField] private GameObject[] playerObjects;
    [SerializeField] private GameObject[] tagObjects;
    
    [Header("Settings")]
    [SerializeField] private MMF_Player _changeSceneFeedback;

    private void Start()
    {
        var results = GameResultManager.Instance.Results;
        string summary = "";

        for (int i = 0; i < results.Count; i++)
        {
            var result = results[i];
            summary += $"{result.playerName}: " +
                       (result.survived ? $"Sobreviveu" : "Morreu") +
                       "\n";

            // ⚡ Oculta o player e a tag se não sobreviveu
            if (!result.survived)
            {
                if (i < playerObjects.Length && playerObjects[i] != null)
                    playerObjects[i].SetActive(false);

                if (i < tagObjects.Length && tagObjects[i] != null)
                    tagObjects[i].SetActive(false);
            }
        }

        Debug.Log("Resultados do Jogo:\n" + summary);
        
        CenterWinners();
    }

    private void Update()
    {
        // Se o jogador clicar em qualquer lado ou pressionar qualquer tecla
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            ContinueToNext();
        }
    }

    private void ContinueToNext()
    {
        // ⚙️ Aqui fazes o que quiseres — neste caso, voltar à cena inicial
        _changeSceneFeedback?.PlayFeedbacks();
    }
    
    private void CenterWinners()
    {
        // Lista de players ativos (sobreviventes)
        var activePlayers = new System.Collections.Generic.List<GameObject>();
        foreach (var player in playerObjects)
        {
            if (player.activeSelf)
                activePlayers.Add(player);
        }

        int count = activePlayers.Count;
        if (count == 0) return;

        // Define o espaçamento máximo entre jogadores
        float totalWidth = 4.5f; // exemplo: espaço total que queres usar
        float spacing = count > 1 ? totalWidth / (count - 1) : 0f;

        // Começa do centro menos metade da largura total
        float startX = -spacing * (count - 1) / 2f;

        for (int i = 0; i < count; i++)
        {
            var player = activePlayers[i];
            var pos = player.transform.position;
            pos.x = startX + spacing * i;
            player.transform.position = pos;
        }
    }
}
