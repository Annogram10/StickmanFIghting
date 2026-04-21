using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpHoldForce = 25f;
    public float maxJumpHoldTime = 0.25f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping =false;
    private float jumpHoldTimer = 0f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = (Input.GetKey(KeyCode.A) ? -1f : 0f) + (Input.GetKey(KeyCode.D) ? 1f : 0f);
        rb.linearVelocity = new Vector2(moveInput * MoveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            isJumping = true; 
            jumpHoldTimer = 0f;
        }

        if(Input.GetKey(KeyCode.W) && isJumping)
        {
            if (jumpHoldTimer < maxJumpHoldTime)
            {
                jumpHoldTimer += Time.deltaTime;
                jumpHoldTimer = Mathf.Clamp(jumpHoldTimer, 0f, maxJumpHoldTime);
            }

        }
        if (Input.GetKeyUp(KeyCode.W) && isJumping)
        {
            
            float chargePercent = jumpHoldTimer / maxJumpHoldTime; 
            float totalForce = jumpForce + (jumpHoldForce*chargePercent);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, totalForce);
            isJumping = false;
            jumpHoldTimer = 0f;
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


