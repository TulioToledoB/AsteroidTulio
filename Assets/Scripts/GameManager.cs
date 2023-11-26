
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    public int currentScore;
    public int bestScore;
    public Image[] livesImages;
    public int livesRemaining; 
    public TextMeshProUGUI score; 
    public TextMeshProUGUI bestScoreText;    
    public GameObject gameOver;
    public GameObject gameElements;


private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }



    private void Start()
    {
        livesRemaining = livesImages.Length; 
        UpdateLivesDisplay(); 
        
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        currentScore = 0; 
        UpdateScoreUI();  
        gameOver.SetActive(false);
        gameElements.SetActive(true);
    }

    public void Restart (){
        Destroy(instance.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void BackMenu (){
     Destroy(instance.gameObject);
        SceneManager.LoadScene("Menu");
        
    }

   
    public void LoseLife()
    {
        if (livesRemaining > 0)
        {
            livesRemaining--;
            UpdateLivesDisplay();

            if (livesRemaining == 0)
            {
                 if (livesRemaining == 0)
            {
               
                    HandleGameOver();
            }
            }
        }
    }

  
    private void UpdateLivesDisplay()
    {
        for (int i = 0; i < livesImages.Length; i++)
        {
            livesImages[i].enabled = i < livesRemaining;
        }
    }

    private void UpdateScoreUI() {
    score.text = "Score: " + currentScore.ToString();
    bestScoreText.text = "Best Score: " + bestScore.ToString();
}
    private void CheckForBestScore() {
            if (currentScore > bestScore) {
                bestScore = currentScore;
                PlayerPrefs.SetInt("BestScore", bestScore);
               
            }
    }
    public void AddScore(int scoreToAdd) {
        currentScore += scoreToAdd;
        UpdateScoreUI();
        CheckForBestScore();
    }

private void HandleGameOver()
{
    
    CheckForBestScore();
  
    gameOver.SetActive(true);
    gameElements.SetActive(false);
}    
}