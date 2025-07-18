using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float timeToDestroy;

    private Rigidbody2D m_rb;
    private Vector2 moveDirection = Vector2.right;

    private static List<GameObject> bulletPool;
    private static int poolSize = 15;
    private static GameObject bulletPrefab;

    void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        CancelInvoke(); // Hủy mọi lệnh hẹn giờ trước (nếu có)
        Invoke("Deactivate", timeToDestroy);
    }

    void Update()
    {
        m_rb.velocity = moveDirection.normalized * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    // ---------- Pooling ----------

    public static void InitPool(GameObject prefab)
    {
        if (bulletPool != null) return; // Đã khởi tạo rồi

        bulletPrefab = prefab;
        bulletPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public static GameObject GetBullet()
    {
        if (bulletPool == null)
        {
            Debug.LogError("Bullet pool chưa được khởi tạo. Gọi Bullet.InitPool(prefab) trước.");
            return null;
        }

        foreach (GameObject b in bulletPool)
        {
            if (!b.activeInHierarchy)
            {
                return b;
            }
        }

        // Nếu tất cả bận, có thể mở rộng pool tại đây (nếu muốn)
        return null;
    }
}



