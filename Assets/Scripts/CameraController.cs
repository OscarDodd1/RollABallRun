using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float smoothTime = 0.2f;

    [Header("FOV Settings")]
    public float minFOV = 60f;
    public float maxFOV = 65;
    public float maxSpeed = 30f;
    public float fovSmoothSpeed = 5f;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;
    private Rigidbody playerRb;

    void Start()
    {
        offset = transform.position - player.transform.position;
        cam = GetComponent<Camera>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (player == null || playerRb == null) return;

        // --- Camera follow ---
        Vector3 desiredPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );

        // --- Speed-based FOV ---
        Vector3 horizontalVel = playerRb.linearVelocity;
        horizontalVel.y = 0f;
        float speed = horizontalVel.magnitude;

        float t = Mathf.Clamp01(speed / maxSpeed);
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, t);

        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView,
            targetFOV,
            Time.deltaTime * fovSmoothSpeed
        );
    }
}
