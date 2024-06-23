using UnityEngine;

public class NewUIObjectMover : MonoBehaviour
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
        Vector2 newPosition = uiObject.anchoredPosition + _direction * speed * Time.deltaTime;

        // Calculate the offset from the center
        Vector2 offset = newPosition - _center;

        // Check if the new position is outside the radius
        if (offset.magnitude > radius)
        {
            // Reflect the direction based on the boundary edge
            _direction = Vector2.Reflect(_direction, offset.normalized);

            // Ensure the new position is within the radius by correcting it
            offset = offset.normalized * radius;
            newPosition = _center + offset;
        }

        // Move the UI object to the new position
        uiObject.anchoredPosition = newPosition;
    }
}