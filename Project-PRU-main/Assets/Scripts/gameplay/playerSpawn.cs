using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawn : MonoBehaviour
{
    public Transform spawnPointP1, spawnPointP2;
    public GameObject[] characterPrefabs;

    void Start()
    {
        int p1Index = PlayerPrefs.GetInt("P1Char", 0);
        int p2Index = PlayerPrefs.GetInt("P2Char", 1);

        Instantiate(characterPrefabs[p1Index], spawnPointP1.position, Quaternion.identity);
        Instantiate(characterPrefabs[p2Index], spawnPointP2.position, Quaternion.identity);
    }
}
