using TMPro;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public TextMeshProUGUI bestRanksText;

    int maxScores = 20;

    public int SaveScore(string playerName, float playerTime)
    {
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);

        if (scoreCount < maxScores)
        {
            PlayerPrefs.SetString("ScoreName" + scoreCount, playerName);
            PlayerPrefs.SetFloat("ScoreTime" + scoreCount, playerTime);
            scoreCount++;
            PlayerPrefs.SetInt("ScoreCount", scoreCount);
        }
        else
        {
            int worstIndex = GetWorstScoreIndex(scoreCount);
            float worstTime = PlayerPrefs.GetFloat("ScoreTime" + worstIndex);

            if (playerTime < worstTime)
            {
                PlayerPrefs.SetString("ScoreName" + worstIndex, playerName);
                PlayerPrefs.SetFloat("ScoreTime" + worstIndex, playerTime);
            }
        }

        PlayerPrefs.Save();

        return GetPlayerRank(playerName, playerTime);
    }

    public void UpdateBestRanksText()
    {
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);

        string[] names = new string[scoreCount];
        float[] times = new float[scoreCount];

        for (int i = 0; i < scoreCount; i++)
        {
            names[i] = PlayerPrefs.GetString("ScoreName" + i, "---");
            times[i] = PlayerPrefs.GetFloat("ScoreTime" + i, 99999f);
        }

        SortScores(names, times);

        string text = "BEST RANKS\n\n";

        int showCount = Mathf.Min(3, scoreCount);

        for (int i = 0; i < showCount; i++)
        {
            text += (i + 1) + ". " + names[i] + " - " + times[i].ToString("F2") + " sn\n";
        }

        for (int i = showCount; i < 3; i++)
        {
            text += (i + 1) + ". ---\n";
        }

        bestRanksText.text = text;
    }

    int GetWorstScoreIndex(int scoreCount)
    {
        int worstIndex = 0;
        float worstTime = PlayerPrefs.GetFloat("ScoreTime0", 0f);

        for (int i = 1; i < scoreCount; i++)
        {
            float time = PlayerPrefs.GetFloat("ScoreTime" + i);

            if (time > worstTime)
            {
                worstTime = time;
                worstIndex = i;
            }
        }

        return worstIndex;
    }

    int GetPlayerRank(string playerName, float playerTime)
    {
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);

        int rank = 1;

        for (int i = 0; i < scoreCount; i++)
        {
            float time = PlayerPrefs.GetFloat("ScoreTime" + i);

            if (time < playerTime)
            {
                rank++;
            }
        }

        return rank;
    }

    void SortScores(string[] names, float[] times)
    {
        for (int i = 0; i < times.Length - 1; i++)
        {
            for (int j = i + 1; j < times.Length; j++)
            {
                if (times[j] < times[i])
                {
                    float tempTime = times[i];
                    times[i] = times[j];
                    times[j] = tempTime;

                    string tempName = names[i];
                    names[i] = names[j];
                    names[j] = tempName;
                }
            }
        }
    }
}