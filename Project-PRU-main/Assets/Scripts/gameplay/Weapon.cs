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
            if (player != null && player.currentWeapon == null)
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

    public void Fire(float direction)
    {
        if (bulletPrefab == null || firePoint == null || direction == 0) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float dir = direction > 0 ? 1f : -1f;
            rb.velocity = new Vector2(dir * fireForce, 0f);
        }

        // Gán người bắn vào viên đạn
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.owner = owner;
        }
    }
}
