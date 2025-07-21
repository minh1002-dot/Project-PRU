using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            PlayerControler player = other.GetComponent<PlayerControler>();
            if (player != null && player.currentWeapon == null)
            {
                player.PickUpWeapon(transform.parent.gameObject); // gán weapon cha
            }
        }
    }
}
