using UnityEngine;

public class Move : MonoBehaviour
{
    public float main1;
    public bool facingRight = true;
    public float speed = 7f;
    public Rigidbody rb;

    [Header("Jumping Settings")]
    public float jumpForce = 8f;         // Adjust this value in the inspector to change the jump height
    public Transform groundCheck;        // An empty GameObject placed at the player's feet
    public LayerMask groundLayer;        // Set this to your Ground layer in the inspector
    private bool isGrounded;
    private bool jumpRequest;
    public float fallMultiplier = 2.5f;

    [Space(12)]
    public Animator anim;

    void Update()
    {
        main1 = Input.GetAxis("Horizontal");

        float flipF = facingRight ? 1 : -1;

        anim.SetFloat("Speed", Mathf.Abs(main1 * flipF));

        if (main1 > 0 && !facingRight)
        {
            Flip();
        }
        else if (main1 < 0 && facingRight)
        {
            Flip();
        }

        // 1. Check if the player is touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
        anim.SetBool("isGrounded", isGrounded);

        // 2. Catch the jump input (prevents missed inputs in FixedUpdate)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;

            anim.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(main1 * speed, rb.linearVelocity.y, rb.linearVelocity.z);

        if (jumpRequest)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            jumpRequest = false;
        }

        if (rb.linearVelocity.y < 3f && !isGrounded)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        float yRotation = facingRight ? 90f : -90f;
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}