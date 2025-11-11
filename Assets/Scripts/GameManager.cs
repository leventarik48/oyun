using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public bool isGameOver = false;
    public float finalDistance = 0f;

    [Header("References")]
    public CarController carController;
    public UIManager uiManager;

    private static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        isGameOver = false;

        if (carController == null)
            carController = FindObjectOfType<CarController>();

        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if (isGameOver)
        {
            // Restart on R key
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }

            // Quit on Escape
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
        }
    }

    public void OnPlayerDeath(float distance)
    {
        isGameOver = true;
        finalDistance = distance;

        // Slow down time for dramatic effect
        Time.timeScale = 0.3f;

        // Show game over UI
        if (uiManager != null)
        {
            uiManager.ShowGameOver(distance);
        }

        Debug.Log("Player died! Distance: " + distance.ToString("F1") + "m");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
}
