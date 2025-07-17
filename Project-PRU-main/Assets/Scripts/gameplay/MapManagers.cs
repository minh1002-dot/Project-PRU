using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagers : MonoBehaviour
{
    public GameObject[] mapPrefabs;
    void Start()
    {
        int mapIndex = PlayerPrefs.GetInt("SelectedMap", 0);
        Instantiate(mapPrefabs[mapIndex], Vector3.zero, Quaternion.identity);
    }
}
