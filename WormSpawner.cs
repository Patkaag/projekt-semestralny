using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSpawner : MonoBehaviour
{
    public GameObject wormPrefab; // Prefab robaka
    public float spawnInterval = 3f; // Czas miêdzy spawnami
    public float spawnRangeX = 10f; // Zakres pozycji X
    public int maxWorms = 10; // Maksymalna liczba robaków

    private int currentWormCount = 0; // Aktualna liczba robaków

    void Start()
    {
        // Rozpoczêcie generowania robaków
        StartCoroutine(SpawnWorms());
    }

    IEnumerator SpawnWorms()
    {
        while (true)
        {
            if (wormPrefab != null && currentWormCount < maxWorms)
            {
                float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);

                // Pobierz górn¹ granicê kamery
                Vector3 topOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));
                topOfScreen.z = 0; // Ustaw Z na 0, poniewa¿ pracujemy w przestrzeni 2D

                Vector2 spawnPosition = new Vector2(spawnPosX, topOfScreen.y);

                Instantiate(wormPrefab, spawnPosition, Quaternion.identity);
                currentWormCount++;
            }
            else if (wormPrefab == null)
            {
                Debug.LogWarning("Worm Prefab is missing!");
            }

            // Czekaj okreœlony czas miêdzy spawnami
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void DecreaseWormCount()
    {
        if (currentWormCount > 0)
        {
            currentWormCount--;
        }
    }
}
