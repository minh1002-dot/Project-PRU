using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int maxJumps = 2;
    private int jumpCount;
    private Rigidbody2D rb;

    public Transform player1;              // Tham chiếu tới Player1
    public float attackRange = 2.5f;
    public float attackForce = 30f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        jumpCount = maxJumps;
    }

    void Update()
    {
        // Di chuyển ← →
        float move = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) move = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) move = 1f;

        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Nhảy
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }

        // Tấn công khi nhấn K
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K pressed");
            if (player1 == null)
            {
                Debug.Log("player1 is null");
            }
            float distance = Vector2.Distance(transform.position, player1.position);
            if (distance <= attackRange)
            {
                Rigidbody2D enemyRb = player1.GetComponent<Rigidbody2D>();
                Vector2 pushDir = (player1.position - transform.position).normalized;
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

