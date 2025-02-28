using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realm_moving_platform : MonoBehaviour
{
    [SerializeField] private float moveDistance = 4.5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private RealmShift realmShift;

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
        if (movingRight && CanUseAbilities())
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition && CanUseAbilities())
            {
                movingRight = false;
                targetPosition = startPosition - new Vector3(moveDistance, 0, 0);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition && CanUseAbilities())
            {
                movingRight = true;
                targetPosition = startPosition + new Vector3(moveDistance, 0, 0);
            }
        }
    }
    private bool CanUseAbilities()
    {
        return realmShift != null && realmShift.isRealmShifted;
    }
}
