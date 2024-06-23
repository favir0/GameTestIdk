using System.Collections;
using UnityEngine;

public class JiggleEffect : MonoBehaviour
{
    [SerializeField] private RectTransform targetObject; // Объект, который будет джигглиться
    public float jiggleDuration = 0.5f; // Продолжительность эффекта
    public float jiggleMagnitude = 0.1f; // Величина джиггла
    public float jiggleDelay = 0.05f;

    private Coroutine jiggleCoroutine;

    public void StartJiggle()
    {
        if (jiggleCoroutine != null)
        {
            StopCoroutine(jiggleCoroutine);
        }

        jiggleCoroutine = StartCoroutine(Jiggle());
    }

    private IEnumerator Jiggle()
    {
        if (targetObject == null)
        {
            yield break; // Выход из корутины, если targetObject не назначен
        }

        Vector3 originalPosition = targetObject.anchoredPosition;
        float elapsed = 0.0f;

        while (elapsed < jiggleDuration)
        {
            float x = Random.Range(-1f, 1f) * jiggleMagnitude;
            float y = Random.Range(-1f, 1f) * jiggleMagnitude;

            targetObject.anchoredPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(jiggleDelay);
            elapsed += jiggleDelay;
            yield return null;
        }

        targetObject.anchoredPosition = originalPosition;
        jiggleCoroutine = null;
    }
}