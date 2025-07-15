using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int maxJumps = 2;
    private int jumpCount;
    private Rigidbody2D rb;

    public Transform player2;              // Tham chiếu tới Player2
    public float attackRange = 2.5f;       // Phạm vi tấn công
    public float attackForce = 30f;         // Lực đẩy khi trúng đòn

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        jumpCount = maxJumps;
    }

    void Update()
    {
        // Di chuyển A/D
        float move = 0f;
        if (Input.GetKey(KeyCode.A)) move = -1f;
        else if (Input.GetKey(KeyCode.D)) move = 1f;

        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Nhảy
        if (Input.GetKeyDown(KeyCode.W) && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }

        // Tấn công khi nhấn V
        if (Input.GetKeyDown(KeyCode.V))
        {
            float distance = Vector2.Distance(transform.position, player2.position);
            if (distance <= attackRange)
            {
                Rigidbody2D enemyRb = player2.GetComponent<Rigidbody2D>();
                Vector2 pushDir = (player2.position - transform.position).normalized;
                enemyRb.velocity = new Vector2(pushDir.x * attackForce, enemyRb.velocity.y);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            jumpCount = maxJumps;
        }
    }
}



