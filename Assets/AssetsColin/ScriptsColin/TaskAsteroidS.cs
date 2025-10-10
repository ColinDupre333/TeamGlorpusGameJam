using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskAsteroidS : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //start the asteroid task
            PlayerPrefs.SetInt("CurrentSeconds", gameManager.seconds);
            PlayerPrefs.SetInt("CurrentMinutes", gameManager.minutes);
            PlayerPrefs.SetFloat("CurrentTasksToDo", gameManager.tasksToDo);
            PlayerPrefs.SetFloat("CurrentTasksCompleted", gameManager.tasksCompleted);
            PlayerPrefs.SetFloat("CurrentTimeBetweenTasks", gameManager.timeBetweenTasks);
            PlayerPrefs.SetFloat("CurrentDangerMeter", gameManager.DangerMeter.value);
            PlayerPrefs.Save();

            

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            SceneManager.LoadScene("asteroide");
        }
    }
}
