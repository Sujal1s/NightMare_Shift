using UnityEngine;

public class Up_down_Platform : MonoBehaviour
{ 
    [SerializeField] private float moveDistance = 4.5f;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingup = true;

    void Start()    
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, moveDistance, 0);
    }

    void Update()
    {
        if (movingup)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                movingup = false;
                targetPosition = startPosition - new Vector3(0, moveDistance, 0);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                movingup = true;
                targetPosition = startPosition + new Vector3(0, moveDistance, 0);
            }
        }
    }
}