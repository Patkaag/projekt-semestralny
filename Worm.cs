using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public float fallSpeed = 0.2f; // Prêdkoœæ opadania
    public int points = 1;

    private WormSpawner wormSpawner;

    void Start()
    {
        wormSpawner = FindObjectOfType<WormSpawner>();
        if (wormSpawner == null)
        {
            Debug.LogError("WormSpawner not found in the scene.");
        }
    }

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy obiekt koliduj¹cy to rybka
        if (other.CompareTag("Fish"))
        {
            // Zjedzenie robaka
            Fish fish = other.GetComponent<Fish>();
            if (fish != null)
            {
                fish.Eat(points);
                Destroy(gameObject); // Niszczenie tylko instancji robaka

                if (wormSpawner != null)
                {
                    wormSpawner.DecreaseWormCount();
                }
            }
        }
    }

    void OnBecameInvisible()
    {
        // Usuwanie robaka, gdy przestanie byæ widoczny przez kamerê
        if (wormSpawner != null)
        {
            wormSpawner.DecreaseWormCount();
        }
        Destroy(gameObject);
    }
}
