using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int points = 0; // Punkty zdobywane przez rybkê
    private ScoreManager scoreManager;

    public float sizeIncrement = 0.1f; // Iloœæ o jak¹ rybka zwiêksza swój rozmiar
    public int pointsToGrow = 10; // Iloœæ punktów potrzebna do zwiêkszenia rozmiaru
    private int pointsAtLastGrowth = 0; // Punkty podczas ostatniego zwiêkszenia rozmiaru

    private Vector3 initialScale; // Pocz¹tkowa skala rybki

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene.");
        }

        // Zachowaj pocz¹tkow¹ skalê rybki
        initialScale = transform.localScale;
    }

    // Metoda wywo³ywana po zjedzeniu
    public void Eat(int points)
    {
        // Dodaj punkty do wyniku
        if (scoreManager != null)
        {
            scoreManager.AddScore(points);
        }

        // SprawdŸ, czy rybka powinna urosn¹æ
        if (scoreManager != null && (scoreManager.GetScore() - pointsAtLastGrowth) >= pointsToGrow)
        {
            Grow();
            pointsAtLastGrowth = scoreManager.GetScore();
        }

        Debug.Log("Punkty: " + points);
    }

    // Metoda zwiêkszaj¹ca rozmiar rybki
    void Grow()
    {
        // Oblicz wartoœæ przyrostu skali dla x i y
        float growthFactorX = initialScale.x * sizeIncrement;
        float growthFactorY = initialScale.y * sizeIncrement;

        // Zwiêksz skalê rybki proporcjonalnie do wartoœci bezwzglêdnej pocz¹tkowej skali x, zachowuj¹c znak skali x
        transform.localScale = new Vector3(
            transform.localScale.x + (transform.localScale.x > 0 ? growthFactorX : -growthFactorX),
            transform.localScale.y + growthFactorY,
            transform.localScale.z
        );

        Debug.Log("Rybka uros³a! Aktualny rozmiar: " + transform.localScale);
    }
}
