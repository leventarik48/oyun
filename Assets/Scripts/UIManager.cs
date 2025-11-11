using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Game UI")]
    public Text distanceText;
    public Text speedText;
    public GameObject gameUI;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public Text gameOverDistanceText;
    public Text gameOverMessageText;
    public Button continueButton;
    public Text continueButtonText;

    [Header("References")]
    public CarController carController;
    public GameManager gameManager;

    private bool gameOverShown = false;

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (gameUI != null)
            gameUI.SetActive(true);

        gameOverShown = false;

        if (carController == null)
            carController = FindObjectOfType<CarController>();

        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        // Setup continue button
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueButtonClicked);
            continueButton.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (carController != null && carController.IsAlive())
        {
            UpdateGameUI();
        }
    }

    void UpdateGameUI()
    {
        if (distanceText != null)
        {
            float distance = carController.GetDistanceTraveled();
            distanceText.text = "Distance: " + distance.ToString("F1") + "m";
        }

        if (speedText != null)
        {
            float speed = carController.GetSpeed();
            speedText.text = "Speed: " + speed.ToString("F1") + " m/s";
        }
    }

    public void ShowGameOver(float finalDistance, bool canContinue = false)
    {
        if (gameOverShown) return;
        gameOverShown = true;

        if (gameUI != null)
            gameUI.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverDistanceText != null)
        {
            gameOverDistanceText.text = "Distance: " + finalDistance.ToString("F1") + "m";
        }

        if (gameOverMessageText != null)
        {
            string message = GetDeathMessage(finalDistance, canContinue);
            gameOverMessageText.text = message;
        }

        // Show/hide continue button
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(canContinue);

            if (canContinue && continueButtonText != null)
            {
                continueButtonText.text = "Watch Ad to Continue";
            }
        }
    }

    public void HideGameOver()
    {
        gameOverShown = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (gameUI != null)
            gameUI.SetActive(true);
    }

    void OnContinueButtonClicked()
    {
        if (gameManager != null)
        {
            gameManager.ContinueWithAd();
        }
    }

    string GetDeathMessage(float distance, bool canContinue)
    {
        string[] messages = {
            "Saçma sapan hareketler yaptın!",
            "Araba uçmaya çalıştı!",
            "Fizik kurallarını hiçe saydın!",
            "Tepetaklak oldun!",
            "Bu nasıl sürüş böyle?!",
            "Daha dikkatli ol!",
            "Araba değil roket mu sürdün?",
            "Kafayı yedi araba!",
            "İnanılmaz bir kaza!",
            "Hill Climb şampiyonu olamadın!"
        };

        string baseMessage = messages[Random.Range(0, messages.Length)];
        string controls = "\n\nPress R to Restart";

        if (!canContinue)
        {
            controls += "\nPress ESC to Quit";
        }

        return baseMessage + controls;
    }
}
