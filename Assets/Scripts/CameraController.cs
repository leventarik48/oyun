using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;

    [Header("Follow Settings")]
    public Vector3 offset = new Vector3(5f, 3f, -10f);
    public float smoothSpeed = 0.125f;
    public bool lookAhead = true;
    public float lookAheadDistance = 5f;

    [Header("Bounds")]
    public bool useBounds = false;
    public float minX = -10f;
    public float maxX = 200f;
    public float minY = -5f;
    public float maxY = 20f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;

        // Look ahead based on car velocity
        if (lookAhead)
        {
            Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
            if (targetRb != null)
            {
                float velocityX = Mathf.Clamp(targetRb.velocity.x, -lookAheadDistance, lookAheadDistance);
                targetPosition.x += velocityX * 0.5f;
            }
        }

        // Apply bounds if enabled
        if (useBounds)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        }

        // Smooth follow
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
