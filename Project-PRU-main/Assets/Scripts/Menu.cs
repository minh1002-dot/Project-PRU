using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("select_player"); 
    }

    public void OnOptionsButton()
    {
        // Mở panel Options (âm thanh, điều khiển...)
        Debug.Log("settings");
    }

    public void OnExitButton()
    {
        Application.Quit(); // Chỉ hoạt động khi build
        Debug.Log("Quit game");
    }
}
