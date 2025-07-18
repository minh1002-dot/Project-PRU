using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public PlayerControler owner;

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(owner.transform.localScale.x * bulletSpeed, 0);
        bullet.tag = "Bullet_" + owner.tag;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.GetComponent<PlayerControler>().currentWeapon == null)
        {
            col.GetComponent<PlayerControler>().PickUpWeapon(gameObject);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
