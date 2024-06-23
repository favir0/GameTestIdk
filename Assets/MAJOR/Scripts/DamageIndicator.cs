using System.Collections;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public Transform canvasTransform;

    [SerializeField] private GameObject pivot;
    [SerializeField] private float radius;
    [SerializeField] private float force = 5f; // Сила, применяемая к тексту урона

    public void IndicateDamage(int damage)
    {
        // Создание экземпляра префаба
        GameObject damageTextInstance = Instantiate(damageTextPrefab, canvasTransform);

        // Рандомизация позиции в пределах заданного радиуса от пивота
        Vector3 randomOffset = Random.insideUnitCircle * radius;
        Vector3 randomPosition = pivot.transform.position + randomOffset;

        // Перевод мировых координат в экранные
        //Vector3 screenPosition = Camera.main.WorldToScreenPoint(randomPosition);

        // Установка позиции текста урона
        damageTextInstance.transform.position = randomPosition;

        // Получение компонента TMP_Text и установка текста урона
        TMP_Text damageText = damageTextInstance.GetComponent<TMP_Text>();
        damageText.text = damage.ToString();

        // Установка случайного цвета
        damageText.color = new Color(Random.value, Random.value, Random.value);

        // Применение силы для движения текста урона в случайном направлении
        Rigidbody2D rb = damageTextInstance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            rb.AddForce(randomDirection * force, ForceMode2D.Impulse);
        }

        // Запуск корутины для уничтожения текста через некоторое время
        StartCoroutine(DestroyAfterTime(damageTextInstance, 1f));
    }

    private IEnumerator DestroyAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}