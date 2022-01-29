using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button resetScoreButton;

    public static bool isGameOver;
    
    void Start()
    {
        Time.timeScale = 0f;
        startPanel.SetActive(true);
        mainPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        
        restartButton.onClick.AddListener(OnRestart);
        resetScoreButton.onClick.AddListener(OnReset);
    }

    // Remove saved data
    private void OnReset()
    {
        PlayerPrefs.DeleteAll();
    }

    // Restart the level
    private void OnRestart()
    {
        isGameOver = false;
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    void Update()
    {
        // Start the game by pressing any key
        if (Input.anyKey && Time.timeScale == 0f)
        {
            startPanel.SetActive(false);
            mainPanel.SetActive(true);
            Time.timeScale = 1f;
        }

        // Exit the game by pressing escape
        if (isGameOver)
        {
            Time.timeScale = 0f;
            mainPanel.SetActive(false);
            gameOverPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }   
}
