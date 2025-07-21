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
    private Animator animator;
    private Vector3 originalScale;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
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

            if (Input.GetKeyDown(KeyCode.O))
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
            transform.localScale = new Vector3(originalScale.x * facingDirection, originalScale.y, originalScale.z);
        }
        UpdateAnimation();
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
        currentWeapon = weaponObj.GetComponent<Weapon>();
        currentWeapon.owner = this;

        // Tắt physics để dính tay
        Collider2D col = currentWeapon.GetComponent<Collider2D>();
        if (col) col.enabled = false;
        Rigidbody2D rbW = currentWeapon.GetComponent<Rigidbody2D>();
        if (rbW) rbW.simulated = false;
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
                rbW.simulated = true;
                rbW.velocity = new Vector2(move * 2f, 2f); // Ném theo hướng đang di chuyển
            }

            currentWeapon.owner = null;
            currentWeapon = null;
        }

    }
    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.velocity.x) > 0.1;
        bool isJumping = Mathf.Abs(rb.velocity.y) > 0.1;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);

    }
}
