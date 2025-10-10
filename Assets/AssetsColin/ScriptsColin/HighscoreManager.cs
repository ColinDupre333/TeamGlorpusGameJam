using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    int highscoreSeconds = 0;
    GameManager gameManager;


    void Start()
    {
        gameManager = GetComponent<GameManager>();
        highscoreSeconds = PlayerPrefs.GetInt("Highscore", 0);
    }

    void Update()
    {
        if(gameManager.gameEnded)
        {
            int currentTimeInSeconds = (gameManager.minutes * 60) + gameManager.seconds;
            if (currentTimeInSeconds > highscoreSeconds)
            {
                highscoreSeconds = currentTimeInSeconds;
                PlayerPrefs.SetInt("Highscore", highscoreSeconds);
                PlayerPrefs.Save();
            }
        }
    }
}
