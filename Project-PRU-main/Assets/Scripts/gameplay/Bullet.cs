using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20f;
    public PlayerControler owner;

    void OnTriggerEnter2D(Collider2D col)
    {
        // Trúng người chơi?
        if (col.CompareTag("Player1") || col.CompareTag("Player2"))
        {
            PlayerControler hitPlayer = col.GetComponent<PlayerControler>();

            // Không gây sát thương nếu bắn chính mình
            if (hitPlayer != null && hitPlayer != owner)
            {
                Health hp = hitPlayer.GetComponent<Health>();
                if (hp != null)
                {
                    hp.TakeDamage(damage);
                }

                Destroy(gameObject);
            }
        }
    }
}
