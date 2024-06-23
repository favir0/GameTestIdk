using UnityEngine;

public class UIObjectMover : MonoBehaviour
{
    public RectTransform uiObject; // The UI object to move
    public float radius = 100f;    // The radius within which the object should move
    public float speed = 50f;      // The speed of movement

    private Vector2 _center;
    private Vector2 _direction;

    private void Start()
    {
        // Set the initial center to the UI object's current position
        _center = uiObject.anchoredPosition;

        // Set a random initial direction
        _direction = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        MoveUIObject();
    }

    private void MoveUIObject()
    {
        // Calculate the new position
        Vector2 newPosition = uiObject.anchoredPosition + _direction * (speed * Time.deltaTime);

        // Check if the new position is within the radius
        if (Vector2.Distance(_center, newPosition) > radius)
        {
            // Reflect the direction if the new position is outside the radius
            _direction = Vector2.Reflect(_direction, (_center - newPosition).normalized);
        }
        else
        {
            // Move the UI object to the new position
            uiObject.anchoredPosition = newPosition;
        }
    }
}