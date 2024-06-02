using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }
}
