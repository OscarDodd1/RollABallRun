using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float smoothTime = 0.2f; // The time it takes to reach the target
    private Vector3 velocity = Vector3.zero; // Used by SmoothDamp internally

    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    void LateUpdate()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }
}