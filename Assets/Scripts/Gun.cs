using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D m_rb;

    void OnEnable()
    {
        // Khi object được kích hoạt lại từ pool
        StartCoroutine(DeactivateAfterSeconds(7f));
    }

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_rb.velocity = Vector2.down * moveSpeed;
    }

    private System.Collections.IEnumerator DeactivateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
