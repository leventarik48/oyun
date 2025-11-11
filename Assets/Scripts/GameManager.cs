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
    public AdManager adManager;

    [Header("Continue Settings")]
    public bool allowContinueWithAd = true;
    public Vector3 continueSpawnOffset = new Vector3(0, 3, 0);

    private static GameManager instance;
    private bool hasUsedContinue = false;

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
        hasUsedContinue = false;

        if (carController == null)
            carController = FindObjectOfType<CarController>();

        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();

        if (adManager == null)
            adManager = AdManager.Instance;

        // Show banner ad at game start
        if (adManager != null)
        {
#if UNITY_ADS
            adManager.ShowBannerAd();
#endif
        }
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

        // Show interstitial ad (every N deaths)
        if (adManager != null)
        {
#if UNITY_ADS
            adManager.ShowInterstitialAd();
#endif
        }

        // Show game over UI with continue option
        bool canContinue = allowContinueWithAd && !hasUsedContinue && adManager != null;

        if (uiManager != null)
        {
            uiManager.ShowGameOver(distance, canContinue);
        }

        Debug.Log("Player died! Distance: " + distance.ToString("F1") + "m");
    }

    public void ContinueWithAd()
    {
        if (hasUsedContinue || !allowContinueWithAd)
        {
            Debug.LogWarning("Continue already used or not allowed!");
            return;
        }

        if (adManager != null)
        {
#if UNITY_ADS
            adManager.ShowRewardedAd(
                onSuccess: OnContinueAdSuccess,
                onFailed: OnContinueAdFailed
            );
#else
            // Fallback for testing without ads
            OnContinueAdSuccess();
#endif
        }
        else
        {
            Debug.LogWarning("AdManager not found!");
            OnContinueAdFailed();
        }
    }

    void OnContinueAdSuccess()
    {
        Debug.Log("Continue ad watched successfully!");
        hasUsedContinue = true;
        isGameOver = false;
        Time.timeScale = 1f;

        // Respawn car
        if (carController != null)
        {
            // Reset car position slightly above current position
            Vector3 newPosition = carController.transform.position + continueSpawnOffset;
            carController.transform.position = newPosition;
            carController.transform.rotation = Quaternion.identity;

            // Reset velocities
            Rigidbody2D carRb = carController.GetComponent<Rigidbody2D>();
            if (carRb != null)
            {
                carRb.velocity = Vector2.zero;
                carRb.angularVelocity = 0f;
            }

            // Re-enable car (you'll need to add a method to CarController)
            carController.gameObject.SetActive(false);
            carController.gameObject.SetActive(true);
        }

        // Hide game over UI
        if (uiManager != null)
        {
            uiManager.HideGameOver();
        }
    }

    void OnContinueAdFailed()
    {
        Debug.Log("Continue ad failed or cancelled.");
        Time.timeScale = 1f;
        // Just resume game over state
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        hasUsedContinue = false;
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

    public bool HasUsedContinue()
    {
        return hasUsedContinue;
    }
}
