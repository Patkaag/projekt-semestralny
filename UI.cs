using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text pointsText;
    private int points;
    public GameOverScreen gameOverScreen;
    public void GameOver()
    {
        gameOverScreen.Setup();
    }
}
