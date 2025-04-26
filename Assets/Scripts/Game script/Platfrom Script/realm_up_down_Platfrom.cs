using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class realm_up_down_Platfrom : MonoBehaviour
{
    [SerializeField] private float moveDistance = 4.5f;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingup = true;
    [SerializeField] private RealmShift realmShift;
    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, moveDistance, 0);
    }

    void Update()
    {
        if (movingup&& CanUseAbilities())
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition && CanUseAbilities())
            {
                movingup = false;
                targetPosition = startPosition - new Vector3(0, moveDistance, 0);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition && CanUseAbilities())
            {
                movingup = true;
                targetPosition = startPosition + new Vector3(0, moveDistance, 0);
            }
        }
    }
    private bool CanUseAbilities()
    {
        return realmShift != null && realmShift.isRealmShifted;
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