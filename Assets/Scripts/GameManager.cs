
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Una instancia estática para permitir un fácil acceso desde otros scripts.
    public int currentScore;
    public int bestScore;
    public Image[] livesImages; // Referencias a los objetos de imagen que representan las vidas en la UI.
    public int livesRemaining; // La cantidad actual de vidas restantes.
    public TextMeshProUGUI score; // Referencia al texto de la UI para el puntaje actual
    public TextMeshProUGUI bestScoreText;    // Referencia al texto de la UI para el mejor puntaje
    public GameObject gameOver;
    public GameObject gameElements;


private void Awake()
    {
        // Configura el GameManager como un Singleton.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: para persistir entre escenas.
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }



    private void Start()
    {
        livesRemaining = livesImages.Length; // Inicializa con el número total de vidas.
        UpdateLivesDisplay(); // Actualiza la UI de vidas al comenzar.
        // Obtén el mejor puntaje de PlayerPrefs o inicia con 0 si no existe.
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        currentScore = 0; // Inicia el puntaje actual en 0 para una nueva partida.
        UpdateScoreUI();  // Actualiza la UI con los puntajes al iniciar.
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

    // Método para llamar cuando el jugador pierde una vida.
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
                // Manejo del game over:
                    HandleGameOver();
            }
            }
        }
    }

    // Actualiza la representación visual de las vidas.
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
                // Actualiza la UI del mejor puntaje si es necesario.
            }
    }
    public void AddScore(int scoreToAdd) {
        currentScore += scoreToAdd;
        UpdateScoreUI();
        CheckForBestScore();
    }

private void HandleGameOver()
{
    // Aquí manejarías el game over, como mostrar un panel de game over y actualizar el mejor puntaje si es necesario.
    CheckForBestScore();
    // Opcional: Pausa el juego o muestra un botón para reiniciar.
    gameOver.SetActive(true);
    gameElements.SetActive(false);
}    
}