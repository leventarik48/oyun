using UnityEngine;

public class WheelController : MonoBehaviour
{
    [Header("Wheel Settings")]
    public Transform wheelGraphic;
    public bool rotateVisual = true;
    public float visualRotationSpeed = 100f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (wheelGraphic != null && rotateVisual && rb != null)
        {
            // Rotate wheel graphic based on angular velocity
            float rotation = rb.angularVelocity * Time.deltaTime;
            wheelGraphic.Rotate(0, 0, -rotation * visualRotationSpeed);
        }
    }
}
