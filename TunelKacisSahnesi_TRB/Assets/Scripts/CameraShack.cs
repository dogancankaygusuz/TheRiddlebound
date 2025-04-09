using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShack : MonoBehaviour
{
    private Coroutine shakeCamera;
    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    // Sarsmay� baslatma metotu
    public void StartShake(float duration, float magnitude)
    {
        if (shakeCamera != null)
        {
            StopCoroutine(shakeCamera);
        }

        shakeCamera = StartCoroutine(Shake(duration, magnitude));
    }

    // Sarsmay� durdurma metotu
    public void StopShake()
    {
        if (shakeCamera != null)
        {
            StopCoroutine(shakeCamera);
            shakeCamera = null;
        }

        transform.localPosition = originalPos;
    }

    // Sarsmay� sagalayan metot
    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
        shakeCamera = null;
    }
}
