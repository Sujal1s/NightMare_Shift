using UnityEngine;

public class movingplatform : MonoBehaviour
{
    [SerializeField] private float moveDistance = 4.5f;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(moveDistance, 0, 0);
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                movingRight = false;
                targetPosition = startPosition - new Vector3(moveDistance, 0, 0);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                movingRight = true;
                targetPosition = startPosition + new Vector3(moveDistance, 0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}