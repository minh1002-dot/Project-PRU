using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public Image mapImage;
    public TMP_Text mapNameText;

    public Sprite[] mapSprites;
    public string[] mapNames;

    private int currentIndex = 0;

    void Start()
    {
        ShowMap(currentIndex);
    }

    public void NextMap()
    {
        currentIndex = (currentIndex + 1) % mapSprites.Length;
        ShowMap(currentIndex);
    }

    public void PreviousMap()
    {
        currentIndex = (currentIndex - 1 + mapSprites.Length) % mapSprites.Length;
        ShowMap(currentIndex);
    }

    void ShowMap(int index)
    {
        mapImage.sprite = mapSprites[index];
        mapNameText.text = mapNames[index];
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("SelectedMap", currentIndex); // lưu map
        SceneManager.LoadScene("hieu"); 
    }
}
