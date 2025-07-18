using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 6f;
    private Rigidbody2D rb;
    public Transform weaponHolder;
    public Weapon currentWeapon;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = 0f;

        if (gameObject.tag == "Player1")
        {
            if (Input.GetKey(KeyCode.A)) move = -1;
            else if (Input.GetKey(KeyCode.D)) move = 1;
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if (currentWeapon != null && Input.GetKeyDown(KeyCode.G))
            {
                currentWeapon.Fire();
            }
        }
        if (gameObject.tag == "Player2")
        {
            // Dùng Left/Right Arrow
            if (Input.GetKey(KeyCode.LeftArrow)) move = -1;
            else if (Input.GetKey(KeyCode.RightArrow)) move = 1;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if (currentWeapon != null && Input.GetKeyDown(KeyCode.L))
            {
                currentWeapon.Fire();
            }
        }

        // Áp dụng di chuyển
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
    }

    public void PickUpWeapon(GameObject weaponObj)
    {
        weaponObj.transform.SetParent(weaponHolder);
        weaponObj.transform.localPosition = Vector3.zero;
        currentWeapon = weaponObj.GetComponent<Weapon>();
        currentWeapon.owner = this;
    }
}
