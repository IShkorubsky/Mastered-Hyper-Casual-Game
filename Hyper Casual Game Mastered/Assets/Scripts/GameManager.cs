using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    
    public AudioSource ambientMusic;

    public static GameManager Instance;

    private void Start()
    {
        ambientMusic = GetComponent<AudioSource>();
        
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        scoreText.text = ScoreManager.Instance.currentScore.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("Best").ToString();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
