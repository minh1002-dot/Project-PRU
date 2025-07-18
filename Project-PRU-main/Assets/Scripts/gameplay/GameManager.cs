using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.UIElements;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Map")]
    public GameObject[] mapPrefabs;
    private GameObject currentMap;

    [Header("Players")]
    public GameObject[] characterPrefabs;
    public Transform spawnPointP1;
    public Transform spawnPointP2;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: giữ lại khi đổi scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnAll());
    }

    IEnumerator SpawnAll()
    {
        // 1. Spawn map
        int mapIndex = PlayerPrefs.GetInt("SelectedMap", 0);
        currentMap = Instantiate(mapPrefabs[mapIndex], Vector3.zero, Quaternion.identity);
        yield return null;

        // 2. Spawn player
        int p1Index = PlayerPrefs.GetInt("P1Char", 0);
        int p2Index = PlayerPrefs.GetInt("P2Char", 1);

        GameObject player1 = Instantiate(characterPrefabs[p1Index], spawnPointP1.position, Quaternion.identity);
        GameObject player2 = Instantiate(characterPrefabs[p2Index], spawnPointP2.position, Quaternion.identity);

        player1.tag = "Player1";
        player2.tag = "Player2";
    }


}
