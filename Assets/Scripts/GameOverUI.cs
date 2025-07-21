using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text winnerText;
    public Button rematchButton;
    public Button quitButton;

    void Start()
    {
        ShowGameOver(GameManager.Instance.winnerName); 
        rematchButton.onClick.AddListener(ReStart);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void ShowGameOver(string winner)
    {
        gameObject.SetActive(true);
        winnerText.text = winner ;
    }

    void ReStart()
    {
        // Xóa dữ liệu chọn nhân vật
        PlayerPrefs.DeleteKey("P1Character");
        PlayerPrefs.DeleteKey("P2Character");
        PlayerPrefs.DeleteKey("SelectedMap");
        // Load lại scene chọn nhân vật (ví dụ tên là "CharacterSelect")
        SceneManager.LoadScene("Menu");
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
