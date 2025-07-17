using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;

    public int livesP1 = 3;
    public int livesP2 = 3;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayerDied(GameObject player)
    {
        // Giảm mạng, kiểm tra thắng
        // Respawn hoặc load result scene
    }
}
