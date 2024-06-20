using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraController : MonoBehaviour
{
    public float zoomDuration = 1.0f; // 줌인/줌아웃 지속 시간
    public float zoomSize = 5.0f; // 줌인할 때의 카메라 orthographicSize
    public float normalSize = 10.0f; // 기본 카메라 orthographicSize

    private Camera cam;
    private Vector3 originalPosition;
    private float originalSize;

    void Start()
    {
        cam = Camera.main;
        originalPosition = cam.transform.position;
        originalSize = cam.orthographicSize;
    }

    public void ZoomToTarget(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(ZoomTo(target.position));
    }

    public void ResetZoom()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomTo(originalPosition, originalSize));
    }

    private IEnumerator ZoomTo(Vector3 targetPosition, float targetSize = -1)
    {
        if (targetSize < 0)
            targetSize = zoomSize;

        float elapsedTime = 0;
        Vector3 startingPos = cam.transform.position;
        float startingSize = cam.orthographicSize;

        while (elapsedTime < zoomDuration)
        {
            cam.transform.position = Vector3.Lerp(startingPos, targetPosition, elapsedTime / zoomDuration);
            cam.orthographicSize = Mathf.Lerp(startingSize, targetSize, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cam.transform.position = targetPosition;
        cam.orthographicSize = targetSize;
    }
}
