using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        // Display the current time and highest score in the GameOver scene
        float currentTime = PlayerPrefs.GetFloat("CurrentTime");
        float highScore = PlayerPrefs.GetFloat("HighScore");

        currentTimeText.text = "Time: " + FormatTime(currentTime);
        highScoreText.text = "High Score: " + FormatTime(highScore);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
