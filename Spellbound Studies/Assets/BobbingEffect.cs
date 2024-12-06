using UnityEngine;

public class PowerUpShimmer : MonoBehaviour
{
    public float floatAmplitude = 0.2f; // How far it moves up/down
    public float floatSpeed = 2f; // Speed of the floating motion

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Save the starting position
    }

    void Update()
    {
        // Oscillate the Y position to create a floating effect
        transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * floatAmplitude, 0);
    }
}
