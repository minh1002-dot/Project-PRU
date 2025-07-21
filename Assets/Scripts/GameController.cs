using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gun1;      // Gán prefab Gun1 từ Unity Inspector
    public int poolSize = 15;         // Số lượng Gun1 trong pool
    public float spawnY = 7f;         // Độ cao cố định khi spawn
    public float spawnIntervalGun1 = 10f;  // Thời gian giữa các lần spawn

    private List<GameObject> gunPool;
    private float timer;

    void Start()
    {
        // Khởi tạo object pool
        gunPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(gun1);
            obj.SetActive(false);
            gunPool.Add(obj);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnIntervalGun1)
        {
            timer = 0f;
            SpawnGun();
        }
    }

    void SpawnGun()
    {
        // Tìm object không active trong pool
        foreach (GameObject gun in gunPool)
        {
            if (!gun.activeInHierarchy)
            {
                float randomX = Random.Range(-7f, 7f);
                gun.transform.position = new Vector3(randomX, spawnY, 0f);
                gun.SetActive(true);
                break;
            }
        }
    }
}

