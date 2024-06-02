using UnityEngine;

public class EnemyFishAI : MonoBehaviour
{
    public Transform target; // Cel do œledzenia (gracz)
    public float moveSpeed = 2.0f; // Normalna prêdkoœæ ruchu
    public float chaseSpeed = 3.5f; // Prêdkoœæ podczas poœcigu
    public float evadeSpeed = 2.5f; // Prêdkoœæ podczas ucieczki
    public float detectionRadius = 3.0f; // Promieñ wykrywania gracza

    public bool movingRight = true; // Kierunek ruchu
    public bool spawnedFromLeft = true; // Informacja, z której strony zosta³a zespawniona
    public float requiredSizeToEat = 1.0f; // Minimalny rozmiar gracza do zjedzenia tej rybki
    private Camera mainCamera;
    private Vector3 screenBounds;

    public int points = 3;
    public EnemySpawner spawner;
    private ScoreManager scoreManager;

    private void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        scoreManager = FindObjectOfType<ScoreManager>();
        Debug.Log("Rybka zespawnowana na pozycji: " + transform.position);

        // Ustaw odpowiedni¹ orientacjê rybki na pocz¹tku
        if (!spawnedFromLeft && !movingRight || !spawnedFromLeft && movingRight)
        {
            Flip();
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);
        float currentSpeed = moveSpeed;

        if (distanceToPlayer < detectionRadius)
        {
            int playerPoints = scoreManager.GetScore();

            if (points > playerPoints)
            {
                // Œcigaj gracza z wiêksz¹ prêdkoœci¹
                currentSpeed = chaseSpeed;
            }
            else if (points < playerPoints)
            {
                // Unikaj gracza z wiêksz¹ prêdkoœci¹
                currentSpeed = evadeSpeed;
            }
        }

        if (movingRight)
        {
            transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Fish fish = other.GetComponent<Fish>();
            if (fish != null)
            {
                int playerPoints = scoreManager.GetScore();

                if (playerPoints >= requiredSizeToEat)
                {
                    // Gracz mo¿e zjeœæ rybkê
                    fish.Eat(points);
                    Destroy(gameObject);

                    if (spawner != null)
                    {
                        spawner.DecreaseEnemyCount();
                    }
                }
                else
                {
                    // Gracz nie mo¿e zjeœæ rybki, wiêc rybka zjada gracza
                    Destroy(other.gameObject);
                    FindObjectOfType<GameManager>().GameOver();
                }
            }
        }
    }

    void OnBecameInvisible()
    {
        if (spawner != null)
        {
            spawner.DecreaseEnemyCount();
        }
        Destroy(gameObject);
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
