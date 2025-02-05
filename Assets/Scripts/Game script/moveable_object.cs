using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveable_object : MonoBehaviour
{
    public float moveDistance = 4.5f; // Distance to move in the x-axis
    public float moveSpeed = 2f; // Speed of movement

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
}
