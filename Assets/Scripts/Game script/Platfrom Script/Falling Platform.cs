using System;
using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Falling Platform Settings")]
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float destroyDelay = 2f;
    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}