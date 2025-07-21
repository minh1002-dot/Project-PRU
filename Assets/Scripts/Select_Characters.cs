using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CharacterData
{
    public string characterName;
    public RuntimeAnimatorController animatorController;
}

public class Select_Characters : MonoBehaviour
{
    public List<CharacterData> characters;

    // Player 1
    public Animator characterAnimatorP1;
    public TMP_Text characterNameP1;
    private int indexP1 = 0;

    // Player 2
    public Animator characterAnimatorP2;
    public TMP_Text characterNameP2;
    private int indexP2 = 0;

    void Start()
    {
        UpdateUI();
    }

    public void P1Next()
    {
        indexP1 = (indexP1 + 1) % characters.Count;
        UpdateUI();
    }

    public void P1Prev()
    {
        indexP1 = (indexP1 - 1 + characters.Count) % characters.Count;
        UpdateUI();
    }

    public void P2Next()
    {
        indexP2 = (indexP2 + 1) % characters.Count;
        UpdateUI();
    }

    public void P2Prev()
    {
        indexP2 = (indexP2 - 1 + characters.Count) % characters.Count;
        UpdateUI();
    }

    void UpdateUI()
    {
        characterAnimatorP1.runtimeAnimatorController = characters[indexP1].animatorController;
        characterNameP1.text = characters[indexP1].characterName;

        characterAnimatorP2.runtimeAnimatorController = characters[indexP2].animatorController;
        characterNameP2.text = characters[indexP2].characterName;
    }

    public void OnReady()
    {
        PlayerPrefs.SetInt("P1Character", indexP1);
        PlayerPrefs.SetInt("P2Character", indexP2);
        SceneManager.LoadScene("select_map"); 
    }
}
