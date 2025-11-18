using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Image jumpImage;
    public TextMeshProUGUI scoreText;
    public float speed = 0;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private float jumpForce;
    private bool canJump = false;
    private int score;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;

        SetScoreText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetScoreText()
    {
        scoreText.text = score.ToString();
    }

    void FixedUpdate()
    {
        jumpForce = 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                canJump = false;
                jumpForce = 20;

                rb.linearVelocity = new Vector3 (rb.linearVelocity.x, 0, rb.linearVelocity.z);
            }
        }

        Vector3 movement = new Vector3(movementX, jumpForce, movementY);

        rb.AddForce(movement * speed);

        if (canJump)
        {
            jumpImage.rectTransform.sizeDelta = new Vector2(10000000, jumpImage.rectTransform.sizeDelta.y);
        }
        else
        {
            jumpImage.rectTransform.sizeDelta = new Vector2(0, jumpImage.rectTransform.sizeDelta.y);
        }
    }

    void OnTriggerEnter (Collider other) 
    {
        canJump = true;
        if (other.gameObject.CompareTag("Pickup")) 
        {    
            other.gameObject.SetActive(false);
            score += 1;

            SetScoreText();
        }
    }
}