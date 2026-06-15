using UnityEngine;

public class Move : MonoBehaviour
{
    public float main1;
    public bool facingRight = true;
    public float speed = 7f;
    public Rigidbody rb;

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
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(main1 * speed, rb.linearVelocity.y, rb.linearVelocity.z);
    }

    void Flip()
    {
        facingRight = !facingRight;
        float yRotation = facingRight ? 90f : -90f;
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}