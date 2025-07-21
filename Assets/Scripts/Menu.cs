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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
        Debug.Log("Quit game");
    }
}
