using System.Collections;
using TMPro;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject countdownPanel;
    public TextMeshProUGUI countdownText;
    public GameManager gameManager;

    IEnumerator StartCountdown()
    {
        loginPanel.SetActive(false);
        countdownPanel.SetActive(true);
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.text = "";
        countdownText.gameObject.SetActive(false);
        countdownPanel.SetActive(false);

        gameManager.StartGameTimer();
    }

    public void StartButton()
    {
        StartCoroutine(StartCountdown());
    }
}