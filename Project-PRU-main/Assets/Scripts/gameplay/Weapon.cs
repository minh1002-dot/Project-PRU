using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 10f;

    [HideInInspector] public PlayerControler owner;

    private bool isPickedUp = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isPickedUp) return;

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            PlayerControler player = other.GetComponent<PlayerControler>();
            if (player != null)
            {
                player.PickUpWeapon(gameObject);
                isPickedUp = true;

                // Disable Collider & Rigidbody to avoid physics after pickup
                Collider2D col = GetComponent<Collider2D>();
                if (col) col.enabled = false;

                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb) rb.simulated = false;
            }
        }
    }

    public void Fire()
    {
        if (firePoint == null || bulletPrefab == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Xác định hướng bắn dựa theo hướng của player
            float direction = owner.transform.localScale.x > 0 ? 1f : -1f;
            rb.velocity = new Vector2(direction * fireForce, 0f);
        }
    }
}
