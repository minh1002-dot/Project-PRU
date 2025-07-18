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
    private Vector3 originalScale;

    public GameObject bullet1;
    public Transform shootingPoint;

    private bool hasGun = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        jumpCount = maxJumps;

        originalScale = transform.localScale; // lưu scale ban đầu
    }

    private void Update()
    {
        // Di chuyển nếu có input
        float move = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) move = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) move = 1f;

        if (move != 0)
        {
            rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

            // Xoay nhân vật theo hướng di chuyển, giữ nguyên tỷ lệ
            Vector3 scale = originalScale;
            scale.x *= move > 0 ? 1 : -1;
            transform.localScale = scale;
        }

        // Nhảy
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }

        // Tấn công
        if (Input.GetKeyDown(KeyCode.K))
        {
            float distance = Vector2.Distance(transform.position, player1.position);
            if (distance <= attackRange)
            {
                Rigidbody2D enemyRb = player1.GetComponent<Rigidbody2D>();
                Vector2 pushDir = (player1.position - transform.position).normalized;

                // Đẩy theo vòng cung nghiêng lên
                Vector2 arcPushDir = new Vector2(pushDir.x, 0.8f).normalized;

                enemyRb.AddForce(arcPushDir * attackForce, ForceMode2D.Impulse);
            }
        }

        // Bắn
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (hasGun)
                Shooot();
            else
                Debug.Log("Player 2 chưa có súng!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump khi chạm đất
        if (collision.contacts[0].normal.y > 0.5f)
        {
            jumpCount = maxJumps;
        }

        // Xử lý nhặt súng
        if (!hasGun && collision.gameObject.CompareTag("Gun"))
        {
            hasGun = true;
            collision.gameObject.SetActive(false);
            Debug.Log("Player 2 đã nhặt súng!");
        }

        // Va chạm DeathZone - chết
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Player 2 đã chết");
        }
    }

    public void Shooot()
    {
        if (bullet1 && shootingPoint)
        {
            // Khởi tạo pool nếu chưa có
            Bullet.InitPool(bullet1);

            // Lấy viên đạn từ pool
            GameObject bulletObj = Bullet.GetBullet();
            if (bulletObj != null)
            {
                bulletObj.transform.position = shootingPoint.position;
                bulletObj.transform.rotation = Quaternion.identity;
                bulletObj.SetActive(true);

                float dirX = transform.localScale.x > 0 ? 1f : -1f;

                Bullet bulletScript = bulletObj.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.SetDirection(new Vector2(dirX, 0));
                }
            }
        }
    }
}









