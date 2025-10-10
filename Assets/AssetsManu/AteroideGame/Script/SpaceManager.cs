using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceManager : MonoBehaviour
{
    
    public int score = 0;
    public int scoreToWin = 10;
    [SerializeField] AsteroidComponent asteroidPrefab;
    [SerializeField] TMPro.TextMeshProUGUI WinText;
    [SerializeField] TMPro.TextMeshProUGUI ScoreText;
    public int asteroidCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ScoreText.text = score.ToString() + "/" + scoreToWin.ToString();
        WinText.enabled = false;
        if (asteroidCount == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnAsteroid();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = score.ToString() + "/" + scoreToWin.ToString();
        if (asteroidCount == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnAsteroid();
            }
        }
        if(score >= scoreToWin)
        {
            WinText.enabled = true;

            float tasksToDo = PlayerPrefs.GetFloat("CurrentTasksToDo", 0);
            float tasksCompleted = PlayerPrefs.GetFloat("CurrentTasksCompleted", 0);

            PlayerPrefs.SetFloat("CurrentTasksToDo", tasksToDo--);
            PlayerPrefs.SetFloat("CurrentTasksCompleted", tasksCompleted + 1f);
            PlayerPrefs.Save();

            SceneManager.LoadScene("MainGameScene");
        }
    }
    private void SpawnAsteroid()
    {
        float offset = Random.Range(0, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;
        int edge = Random.Range(0, 4);
        if (edge == 0)
        {
            viewportSpawnPosition = new Vector2(offset, 0f);

        }
        else if (edge == 1)
        {
            viewportSpawnPosition = new Vector2(offset, 1f);
        }
        else if (edge == 2)
        {
            viewportSpawnPosition = new Vector2(0f, offset);
        }
        else if (edge == 3)
        {
            viewportSpawnPosition = new Vector2(1f, offset);
        }
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        AsteroidComponent asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        asteroid.spaceManager = this;
    }
}
