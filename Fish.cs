using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int points = 0; // Punkty zdobywane przez rybk�
    private ScoreManager scoreManager;

    public float sizeIncrement = 0.1f; // Ilo�� o jak� rybka zwi�ksza sw�j rozmiar
    public int pointsToGrow = 10; // Ilo�� punkt�w potrzebna do zwi�kszenia rozmiaru
    private int pointsAtLastGrowth = 0; // Punkty podczas ostatniego zwi�kszenia rozmiaru

    private Vector3 initialScale; // Pocz�tkowa skala rybki

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene.");
        }

        // Zachowaj pocz�tkow� skal� rybki
        initialScale = transform.localScale;
    }

    // Metoda wywo�ywana po zjedzeniu
    public void Eat(int points)
    {
        // Dodaj punkty do wyniku
        if (scoreManager != null)
        {
            scoreManager.AddScore(points);
        }

        // Sprawd�, czy rybka powinna urosn��
        if (scoreManager != null && (scoreManager.GetScore() - pointsAtLastGrowth) >= pointsToGrow)
        {
            Grow();
            pointsAtLastGrowth = scoreManager.GetScore();
        }

        Debug.Log("Punkty: " + points);
    }

    // Metoda zwi�kszaj�ca rozmiar rybki
    void Grow()
    {
        // Oblicz warto�� przyrostu skali dla x i y
        float growthFactorX = initialScale.x * sizeIncrement;
        float growthFactorY = initialScale.y * sizeIncrement;

        // Zwi�ksz skal� rybki proporcjonalnie do warto�ci bezwzgl�dnej pocz�tkowej skali x, zachowuj�c znak skali x
        transform.localScale = new Vector3(
            transform.localScale.x + (transform.localScale.x > 0 ? growthFactorX : -growthFactorX),
            transform.localScale.y + growthFactorY,
            transform.localScale.z
        );

        Debug.Log("Rybka uros�a! Aktualny rozmiar: " + transform.localScale);
    }
}
