using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI crystalText;
    public TextMeshProUGUI timeText;

    public GameObject loginPanel;
    public GameObject finishPanel;

    public InputField nameInput;

    public TextMeshProUGUI finishTimeText;
    public TextMeshProUGUI finishRankText;

    public RankingManager rankingManager;

    public int collectedCrystal = 0;
    public int targetCrystal = 5;

    float timer = 0f;
    bool gameStarted = false;
    bool gameFinished = false;

    void Start()
    {
        UpdateCrystalText();
        UpdateTimeText();

        if (finishPanel != null)
            finishPanel.SetActive(false);

        if (rankingManager != null)
            rankingManager.UpdateBestRanksText();
    }

    void Update()
    {
        if (gameStarted && !gameFinished)
        {
            timer += Time.deltaTime;
            UpdateTimeText();
        }
    }

    public void StartGameTimer()
    {
        timer = 0f;
        collectedCrystal = 0;

        gameStarted = true;
        gameFinished = false;

        UpdateCrystalText();
        UpdateTimeText();

        if (loginPanel != null)
            loginPanel.SetActive(false);

        if (finishPanel != null)
            finishPanel.SetActive(false);
    }

    public void AddCrystal()
    {
        if (!gameStarted || gameFinished)
            return;

        collectedCrystal++;
        UpdateCrystalText();

        if (collectedCrystal >= targetCrystal)
        {
            FinishGame();
        }
    }

    void FinishGame()
    {
        gameFinished = true;

        string playerName = nameInput.text;

        if (playerName == "")
            playerName = "Player";

        int rank = rankingManager.SaveScore(playerName, timer);

        finishPanel.SetActive(true);

        finishTimeText.text = "Süreniz: " + timer.ToString("F2") + " sn";
        finishRankText.text = "Sıralamanız: #" + rank;

        rankingManager.UpdateBestRanksText();
    }

    public void BackToMainMenu()
    {
        finishPanel.SetActive(false);
        loginPanel.SetActive(true);

        collectedCrystal = 0;
        timer = 0f;

        gameStarted = false;
        gameFinished = false;

        UpdateCrystalText();
        UpdateTimeText();

        rankingManager.UpdateBestRanksText();
    }

    void UpdateCrystalText()
    {
        crystalText.text = "Crystals\n" + collectedCrystal + " / " + targetCrystal;
    }

    void UpdateTimeText()
    {
        timeText.text = "TIME\n" + timer.ToString("F2");
    }
}