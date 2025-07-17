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

    public Transform player1;
    public float attackRange = 2.5f;
    public float attackForce = 7f;

    private bool isKnocked = false;
    private float knockbackTimer = 0f;
    public float knockbackDuration = 0.3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        jumpCount = maxJumps;
    }

    private void Update()
    {
        float move = 0f;
        if (!isKnocked)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) move = -1f;
            else if (Input.GetKey(KeyCode.RightArrow)) move = 1f;

            if (move != 0)
                rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
            else
                rb.velocity = new Vector2(0f, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount--;
            }
        }
        else
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnocked = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            float distance = Vector2.Distance(transform.position, player1.position);
            if (distance <= attackRange)
            {
                Rigidbody2D enemyRb = player1.GetComponent<Rigidbody2D>();
                Player1 enemyScript = player1.GetComponent<Player1>();

                Vector2 pushDir = (player1.position - transform.position).normalized;
                Vector2 arcPushDir = (new Vector2(pushDir.x, 0.3f)).normalized; // thấp hơn để không bị dựng đứng

                enemyRb.velocity = Vector2.zero; // reset vận tốc
                enemyRb.AddForce(arcPushDir * attackForce, ForceMode2D.Impulse);
                enemyScript.ApplyKnockback(knockbackDuration);
            }
        }
    }

    public void ApplyKnockback(float duration)
    {
        isKnocked = true;
        knockbackTimer = duration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            jumpCount = maxJumps;
        }
    }
}








