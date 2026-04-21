using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float MoveSpeed = 5f; 
    public float jumpForce = 10f; 
    public float jumpHoldForce = 25f;
    public float maxJumpHoldTime = 0.25f;
    private Rigidbody2D rb;
    private bool isGrounded; 
    private bool isJumping = false; 
    private float jumpHoldTimer = 0f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        float moveInput = (Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f) + (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f);
        rb.linearVelocity = new Vector2(moveInput * MoveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            jumpHoldTimer = 0f; 
            isJumping = true;
        }

        if (Input.GetKey(KeyCode.UpArrow) && isJumping)
        {
            jumpHoldTimer += Time.deltaTime;
            jumpHoldTimer = Mathf.Clamp(jumpHoldTimer, 0f, maxJumpHoldTime);
        }

        if(Input.GetKeyUp(KeyCode.UpArrow) && isJumping)
        {
            float chargePercent = jumpHoldTimer / maxJumpHoldTime;
            float totalForce = jumpForce + (jumpHoldForce * chargePercent);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, totalForce);
            isJumping = false;
            jumpHoldTimer =0f;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            isJumping = false;
            jumpHoldTimer = 0f; 
        }
    }
}