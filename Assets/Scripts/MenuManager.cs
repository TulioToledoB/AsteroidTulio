using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    private string mainSceneName = "Game";

    private void Start()
    {
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore", 0).ToString();
    }

    public void StartGame()
{
    // Asegúrate de reiniciar el estado aquí si el GameManager ya existe.
    if (GameManager.instance != null)
    {
        GameManager.instance.Restart();
    }

    SceneManager.LoadScene(mainSceneName);

}
}