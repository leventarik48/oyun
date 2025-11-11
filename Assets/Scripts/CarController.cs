using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Components")]
    public Rigidbody2D carBody;
    public Rigidbody2D frontWheel;
    public Rigidbody2D rearWheel;
    public Transform frontWheelTransform;
    public Transform rearWheelTransform;

    [Header("Movement Settings")]
    public float motorTorque = 500f;
    public float brakeTorque = 300f;
    public float maxSpeed = 30f;
    public float rotationSpeed = 100f;

    [Header("Wheel Settings")]
    public float wheelRadius = 0.5f;
    public float suspensionHeight = 0.3f;
    public float suspensionSpring = 500f;
    public float suspensionDamper = 50f;

    [Header("Death Settings")]
    public Transform driverHead;
    public float maxHeadHeight = 2f;
    public float minHeadHeight = -1f;

    private bool isAlive = true;
    private float distanceTraveled = 0f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        // Create wheel joints if not already set up
        if (carBody == null) carBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isAlive) return;

        CheckDeath();

        // Calculate distance traveled
        distanceTraveled = transform.position.x - startPosition.x;
    }

    void FixedUpdate()
    {
        if (!isAlive) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Apply motor torque to wheels
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            float torque = horizontalInput * motorTorque;

            // Apply torque to wheels
            if (frontWheel != null)
                frontWheel.AddTorque(torque);
            if (rearWheel != null)
                rearWheel.AddTorque(torque);
        }

        // Air rotation control
        if (!IsGrounded() && Mathf.Abs(verticalInput) > 0.1f)
        {
            carBody.AddTorque(-verticalInput * rotationSpeed);
        }

        // Brake
        if (Input.GetKey(KeyCode.Space))
        {
            if (frontWheel != null)
                frontWheel.angularVelocity *= 0.95f;
            if (rearWheel != null)
                rearWheel.angularVelocity *= 0.95f;
        }
    }

    bool IsGrounded()
    {
        // Simple ground check using raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        return hit.collider != null && hit.collider.CompareTag("Ground");
    }

    void CheckDeath()
    {
        if (driverHead == null) return;

        // Check if driver's head hit the ground or is crushed
        Vector3 localHeadPos = transform.InverseTransformPoint(driverHead.position);

        // Death if head is too low (crushed) or car is upside down
        if (localHeadPos.y < minHeadHeight || localHeadPos.y > maxHeadHeight)
        {
            Die();
            return;
        }

        // Check if car is too upside down (z rotation between 90-270 degrees)
        float angle = transform.eulerAngles.z;
        if ((angle > 90 && angle < 270))
        {
            // Allow some time before death
            Invoke("CheckUpsideDown", 2f);
        }

        // Check if head collides with ground
        RaycastHit2D hit = Physics2D.Raycast(driverHead.position, Vector2.down, 0.2f);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            Die();
        }
    }

    void CheckUpsideDown()
    {
        float angle = transform.eulerAngles.z;
        if ((angle > 90 && angle < 270) && isAlive)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isAlive) return;

        isAlive = false;
        Debug.Log("Game Over! Distance: " + distanceTraveled.ToString("F1") + "m");

        // Notify GameManager
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.OnPlayerDeath(distanceTraveled);
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public float GetDistanceTraveled()
    {
        return distanceTraveled;
    }

    public float GetSpeed()
    {
        if (carBody != null)
            return carBody.velocity.magnitude;
        return 0f;
    }
}
