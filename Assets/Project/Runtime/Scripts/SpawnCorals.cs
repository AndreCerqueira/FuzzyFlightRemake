using UnityEngine;
using System.Collections.Generic;

namespace Project.Runtime.Scripts
{
    public class SpawnCorals : MonoBehaviour
    {
        [Header("Área de Spawn (em coordenadas do mundo)")]
        public float minX = -10f;
        public float maxX = 10f;
        public float minZ = -10f;
        public float maxZ = 100f;

        [Header("Altura fixa (Y)")]
        public float fixedY = 0f;
        public float fixedAreiaY = 0f;

        [Header("Prefabs de corais possíveis")]
        public List<GameObject> coralPrefabs = new List<GameObject>();

        [Header("Número de corais a spawnar")]
        public int coralCount = 20;

        [Header("Prefab da areia (plano de 10x10)")]
        public GameObject sandPrefab;

        [Header("Pai opcional (para organização na Hierarchy)")]
        public Transform parentTransform;

        void Start()
        {
            SpawnSand();
            SpawnCoralsInArea();
        }

        // -------------------------------
        // Spawn de corais
        // -------------------------------
        void SpawnCoralsInArea()
        {
            if (coralPrefabs == null || coralPrefabs.Count == 0)
            {
                Debug.LogWarning("Nenhum prefab de coral atribuído!");
                return;
            }

            for (int i = 0; i < coralCount; i++)
            {
                // Escolhe um coral aleatório da lista
                GameObject coralPrefab = coralPrefabs[Random.Range(0, coralPrefabs.Count)];

                float randomX = Random.Range(minX, maxX);
                float randomZ = Random.Range(minZ, maxZ);
                Vector3 spawnPos = new Vector3(randomX, fixedY, randomZ);

                Quaternion randomRot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                Instantiate(coralPrefab, spawnPos, randomRot, parentTransform);
            }
        }

        // -------------------------------
        // Spawn de areia
        // -------------------------------
        void SpawnSand()
        {
            if (sandPrefab == null)
            {
                Debug.LogWarning("Nenhum prefab de areia atribuído!");
                return;
            }

            float planeSize = 10f; // cada plano mede 10 unidades
            int numPlanesZ = Mathf.CeilToInt((maxZ - minZ) / planeSize);
            int numPlanesX = Mathf.CeilToInt((maxX - minX) / planeSize);

            for (int x = 0; x < numPlanesX; x++)
            {
                for (int z = 0; z < numPlanesZ; z++)
                {
                    float posX = minX + x * planeSize + planeSize / 2f - 2f;
                    float posZ = minZ + z * planeSize + planeSize / 2f;
                    Vector3 spawnPos = new Vector3(posX, fixedAreiaY, posZ);

                    Instantiate(sandPrefab, spawnPos, Quaternion.identity, parentTransform);
                }
            }
        }

        // Visualização no editor
        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0f, 0.5f, 1f, 0.3f);
            Vector3 center = new Vector3((minX + maxX) / 2f, fixedY, (minZ + maxZ) / 2f);
            Vector3 size = new Vector3(Mathf.Abs(maxX - minX), 0.1f, Mathf.Abs(maxZ - minZ));
            Gizmos.DrawCube(center, size);
        }
    }
}
