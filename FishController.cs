using UnityEngine;

public class FishTouchController : MonoBehaviour
{
    public float speed = 5f;
    public float size = 1f; // pocz¹tkowy rozmiar rybki
    public float minSize = 1f; // Minimalny rozmiar rybki
    public float maxSize = 2f; // Maksymalny rozmiar rybki
    private Vector3 targetPosition;
    private bool isMoving;
    
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
                targetPosition.z = 0;
                isMoving = true;
            }
        }

        if (isMoving)
        {
            MoveFish();
        }
    }

    private void MoveFish()
    {
        // Poruszaj rybke do celu
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * size * Time.deltaTime);

        Vector3 direction = targetPosition - transform.position;

        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Zatrzymaj rybkê, gdy osi¹gnie cel
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }


    // Funkcja do zmiany rozmiaru rybki
    public void ChangeSize(float amount)
    {
        size += amount;
        size = Mathf.Clamp(size, minSize, maxSize);
        transform.localScale = Vector3.one * size;
    }
}
