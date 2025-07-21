using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.UIElements;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Map")]
    public GameObject[] mapPrefabs;
    private GameObject currentMap;

    [Header("Players")]
    public GameObject[] characterPrefabs;
    public List<CharacterData> characterList;
    public Transform spawnPointP1;
    public Transform spawnPointP2;

    [Header("UI")]
    public UnityEngine.UI.Slider healthSliderP1;
    public UnityEngine.UI.Slider healthSliderP2;

    [HideInInspector] public string winnerName;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
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

        // 2. Spawn players
        int p1Index = PlayerPrefs.GetInt("P1Character", 0);
        Debug.LogError(p1Index);
        int p2Index = PlayerPrefs.GetInt("P2Character", 0);
        Debug.LogError(p2Index);

        GameObject player1 = Instantiate(characterPrefabs[p1Index], spawnPointP1.position, Quaternion.identity);
        Animator anim1 = player1.GetComponent<Animator>();
        if (anim1 != null)
        {
            anim1.runtimeAnimatorController = characterList[p1Index].animatorController;
        }

        GameObject player2 = Instantiate(characterPrefabs[p2Index], spawnPointP2.position, Quaternion.identity);
        Animator anim2 = player2.GetComponent<Animator>();
        if (anim2 != null)
        {
            anim2.runtimeAnimatorController = characterList[p2Index].animatorController;
        }

        player1.tag = "Player1";
        player2.tag = "Player2";

        // Gán slider health sau khi spawn
        Health health1 = player1.GetComponent<Health>();
        Health health2 = player2.GetComponent<Health>();

        if (health1 != null) health1.healthBar = healthSliderP1;
        if (health2 != null) health2.healthBar = healthSliderP2;
    }

    public void PlayerDied(GameObject deadPlayer)
    {
        if (deadPlayer.CompareTag("Player1"))
            winnerName = "Player 2 Wins!";
        else
            winnerName = "Player 1 Wins!";

        // Load Game Over scene
        SceneManager.LoadScene("GameOver");
    }
}
