using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public GameObject[] weaponPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxWeapons = 3;

    private int currentWeaponCount = 0;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentWeaponCount < maxWeapons)
            {
                SpawnWeapon();
            }
        }
    }

    void SpawnWeapon()
    {
        int weaponIndex = Random.Range(0, weaponPrefabs.Length);
        int pointIndex = Random.Range(0, spawnPoints.Length);

        GameObject weapon = Instantiate(weaponPrefabs[weaponIndex], spawnPoints[pointIndex].position, Quaternion.identity);
        currentWeaponCount++;

        // Đặt bộ đếm giảm sau khi weapon bị nhặt (destroy hoặc disable)
        StartCoroutine(WaitForPickupOrTimeout(weapon));
    }

    IEnumerator WaitForPickupOrTimeout(GameObject weapon)
    {
        float timeout = 20f; // auto remove nếu không nhặt sau 20s
        float elapsed = 0f;

        while (elapsed < timeout && weapon != null && weapon.activeInHierarchy)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (weapon != null)
        {
            Destroy(weapon);
            currentWeaponCount--;
        }
    }
}
