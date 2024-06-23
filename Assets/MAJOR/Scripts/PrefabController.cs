using UnityEngine;
using UnityEngine.UI;

public class PrefabController : MonoBehaviour
{
    public float moveSpeed = 50f;       // Speed of movement
    public float lifetime = 5f;         // Lifetime of the prefab before it is destroyed
    private float _timeAlive;           // Time the prefab has been alive
    private Vector2 _direction;         // Direction of movement
    private Image _image;               // Image component for changing alpha

    private void Start()
    {
        _direction = Random.insideUnitCircle.normalized; // Random initial direction
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        Move();
        ChangeAlpha();
        CheckLifetime();
    }

    private void Move()
    {
        transform.Translate(_direction * moveSpeed * Time.deltaTime);
    }

    private void ChangeAlpha()
    {
        _timeAlive += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, _timeAlive / lifetime); // Gradually change alpha from 1 to 0
        if (_image != null)
        {
            Color color = _image.color;
            color.a = alpha;
            _image.color = color;
        }
    }

    private void CheckLifetime()
    {
        if (_timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}