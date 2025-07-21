using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 10f;
    public float cooldownDuration = 3f; // thời gian chờ sau khi bắn 15 viên

    [HideInInspector] public PlayerControler owner;

    public bool isPickedUp = false;
    private int bulletCount = 0;
    private bool isCoolingDown = false;

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
        if (isCoolingDown) return; // đang cooldown thì không bắn
        if (bulletPrefab == null || firePoint == null || direction == 0) return;

        float dir = direction > 0 ? 1f : -1f;

        // Tạo vị trí spawn xa hơn 0.5 đơn vị theo hướng bắn
        Vector3 spawnPosition = firePoint.position + new Vector3(dir * 1.5f, 0f, 0f);

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(dir * fireForce, 0f);
        }
        bulletCount++;
        if (bulletCount >= 15)
        {
            StartCoroutine(StartCooldown());
        }
    }

    private System.Collections.IEnumerator StartCooldown()
    {
        isCoolingDown = true;
        Debug.Log("Cooling down...");
        yield return new WaitForSeconds(cooldownDuration);
        bulletCount = 0;
        isCoolingDown = false;
        Debug.Log("Cooldown complete. You can shoot again.");
    }
}
