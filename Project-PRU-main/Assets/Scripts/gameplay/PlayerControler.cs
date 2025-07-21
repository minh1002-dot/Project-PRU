using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 6f;
    private Rigidbody2D rb;
    public Transform weaponHolder;
    public Weapon currentWeapon;
    private float move = 0f;
    private float facingDirection = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move = 0f; // Reset move mỗi frame
        // Điều khiển cho Player1
        if (gameObject.tag == "Player1")
        {
            if (Input.GetKey(KeyCode.A)) move = -1;
            else if (Input.GetKey(KeyCode.D)) move = 1;

            if (Input.GetKeyDown(KeyCode.W))
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            if (Input.GetKeyDown(KeyCode.G) && currentWeapon != null)
                currentWeapon.Fire(facingDirection);

            if (Input.GetKeyDown(KeyCode.T))
                DropWeapon();
        }

        // Điều khiển cho Player2
        else if (gameObject.tag == "Player2")
        {
            if (Input.GetKey(KeyCode.LeftArrow)) move = -1;
            else if (Input.GetKey(KeyCode.RightArrow)) move = 1;

            if (Input.GetKeyDown(KeyCode.UpArrow))
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            if (Input.GetKeyDown(KeyCode.L) && currentWeapon != null)
                currentWeapon.Fire(facingDirection);

            if (Input.GetKeyDown(KeyCode.O))
                DropWeapon();
        }

        if (move != 0)
        {
            facingDirection = move > 0 ? 1f : -1f;
            transform.localScale = new Vector3(facingDirection, 1f, 1f);
        }
        else
        {
            // Giữ hướng cũ nếu đứng yên
            facingDirection = transform.localScale.x;
        }
    }

    void FixedUpdate()
    {
        if (move == 0)
        {
            // Hãm trượt bằng cách giảm tốc độ về 0
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
        }
    }

    public void PickUpWeapon(GameObject weaponObj)
    {
        if (currentWeapon != null) return;

        weaponObj.transform.SetParent(weaponHolder);
        weaponObj.transform.localPosition = Vector3.zero;

        // Reset rotation để tránh lật úp/lệch
        weaponObj.transform.localRotation = Quaternion.identity;

        currentWeapon = weaponObj.GetComponent<Weapon>();
        currentWeapon.owner = this;

        // Disable physics
        Collider2D col = currentWeapon.GetComponent<Collider2D>();
        if (col) col.enabled = false;

        Rigidbody2D rbW = currentWeapon.GetComponent<Rigidbody2D>();
        if (rbW) rbW.simulated = false;

        // Lật sprite vũ khí theo hướng của player
        SpriteRenderer weaponSprite = weaponObj.GetComponent<SpriteRenderer>();
        if (weaponSprite != null)
        {
            weaponSprite.flipX = (facingDirection < 0);
        }
        else
        {
            // Nếu không dùng flipX thì chỉnh scale
            Vector3 scale = weaponObj.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(facingDirection);
            weaponObj.transform.localScale = scale;
        }
    }

    public void DropWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.transform.SetParent(null);

            Collider2D col = currentWeapon.GetComponent<Collider2D>();
            if (col != null) col.enabled = true;

            Rigidbody2D rbW = currentWeapon.GetComponent<Rigidbody2D>();
            if (rbW != null)
            {
                rbW.bodyType = RigidbodyType2D.Dynamic;
                rbW.simulated = true;
                rbW.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

                float throwDir = move != 0 ? move : transform.localScale.x;
                rbW.velocity = new Vector2(throwDir * 2f, 2f);
            }

            currentWeapon.owner = null;
            Destroy(currentWeapon.gameObject, 20f);
            currentWeapon = null;
        }
    }
}
