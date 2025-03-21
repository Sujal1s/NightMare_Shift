using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Coroutine zoomCoroutine;
    [SerializeField] float zoom;

    private void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("MainCamera is not assigned .");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.CompareTag("Player") )
        {
            StartZoomCoroutine(zoom, 2f);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ( other.CompareTag("Player") )
        {
            StartZoomCoroutine(3.11f, 2f);
        }
       
    }

    private void StartZoomCoroutine(float targetSize, float speed)
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }
        zoomCoroutine = StartCoroutine(ChangeCameraSize(targetSize, speed));
    }

    private IEnumerator ChangeCameraSize(float targetSize, float speed)
    {
        while (Mathf.Abs(mainCamera.orthographicSize - targetSize) > 0.01f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, speed * Time.deltaTime);
            yield return null;
        }
        mainCamera.orthographicSize = targetSize;
    }
}