using UnityEngine;

public class move : MonoBehaviour
{

    public float main1;

    public bool facingRight = true;

    public float speed = 5f;

    public Rigidbody rb;

    void Update()
    {
        main1 = Input.GetAxis("Horizontal");

        if (main1 < 0 && facingRight)
        {
            flip();
        }
        else if (main1 > 0 && !facingRight)
        {
            flip();
        }

    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(main1 * speed, rb.linearVelocity.y);

    }


    void flip()
    {
        if (!facingRight)
        transform.localRotation = Quaternion.Euler(0, -90, 0);
        else transform.localRotation = Quaternion.Euler(0, 90, 0);
        facingRight = !facingRight;

    }
}
