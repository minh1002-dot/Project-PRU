using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public Slider healthBar;

    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar == null)
        {
            if (CompareTag("Player1"))
                healthBar = GameObject.Find("Player1Health").GetComponent<Slider>();
            else if (CompareTag("Player2"))
                healthBar = GameObject.Find("Player2Health").GetComponent<Slider>();
        }

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            
            //GameManager.Instance.PlayerDied(gameObject);
            Destroy(gameObject);
            //them man hinh end game vao day
        }
    }

}
