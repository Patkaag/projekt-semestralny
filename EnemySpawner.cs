using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab wrogiej rybki
    public float spawnInterval = 2f; // Czas miêdzy spawnami
    public float spawnRangeY = 5f;
    public int maxEnemies = 10; // Maksymalna liczba wrogich rybek

    private int currentEnemyCount = 0; // Aktualna liczba wrogich rybek dla tego spawnera
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentEnemyCount < maxEnemies)
            {
                float spawnPosY = Random.Range(-spawnRangeY, spawnRangeY);

                bool spawnFromLeft = Random.Range(0, 2) == 0;

                Vector3 spawnPosition;
                if (spawnFromLeft)
                {
                    // Pobierz lew¹ granicê kamery i dodaj margines do spawnienia poza ekranem
                    Vector3 leftOfScreen = mainCamera.ViewportToWorldPoint(new Vector3(-0.1f, 0.5f, mainCamera.nearClipPlane));
                    leftOfScreen.z = 0;
                    spawnPosition = new Vector2(leftOfScreen.x, spawnPosY);
                }
                else
                {
                    // Pobierz praw¹ granicê kamery i dodaj margines do spawnienia poza ekranem
                    Vector3 rightOfScreen = mainCamera.ViewportToWorldPoint(new Vector3(1.1f, 0.5f, mainCamera.nearClipPlane));
                    rightOfScreen.z = 0;
                    spawnPosition = new Vector2(rightOfScreen.x, spawnPosY);
                }

                Debug.Log("Spawnowanie wrogiej rybki na pozycji: " + spawnPosition);

                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                var enemyAI = newEnemy.GetComponent<EnemyFishAI>();
                enemyAI.movingRight = spawnFromLeft;
                enemyAI.spawnedFromLeft = spawnFromLeft; // Informacja, z której strony zosta³a zespawniona
                enemyAI.spawner = this; // Przekazanie referencji do spawnera
                currentEnemyCount++;
            }
        }
    }

    public void DecreaseEnemyCount()
    {
        if (currentEnemyCount > 0)
        {
            currentEnemyCount--;
        }
    }
}
