using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player1") || col.CompareTag("Player2"))
        {
            col.GetComponent<Health>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
