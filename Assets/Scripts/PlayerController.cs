using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private float jumpForce;
    private bool canJump = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        jumpForce = 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                canJump = false;
                jumpForce = 15;
            }
        }

        Vector3 movement = new Vector3(movementX, jumpForce, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter (Collider other) 
    {
        canJump = true;
        if (other.gameObject.CompareTag("Pickup")) 
        {    
            other.gameObject.SetActive(false);
        }
    }
}