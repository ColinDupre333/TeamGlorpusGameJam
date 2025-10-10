using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text timerText;
    [SerializeField] public Slider DangerMeter;

    //UI
    [SerializeField] Image redScreen;

    //DangerManager
    float _tasksCompleted;
    public float tasksToDo = 0;
    float baseDangerMeterIncrease = 0.001f;
    public float tasksCompleted
    {
        get => _tasksCompleted;
        set
        {
            _tasksCompleted = value;
            DangerMeterIncrease();
        }
    }
    bool inDangerZone = false;
    [SerializeField] GameObject[] fireFx;


    //task manager
    [Header("Tasks")]
    [SerializeField] public GameObject taskAsteroid;
    [SerializeField] public GameObject taskLaser;
    [SerializeField] public GameObject taskFlashlight;
    public float timeBetweenTasks = 10f;

    bool gameStarted = false;
    public bool gameEnded = false;

    public int seconds = 0;
    public int minutes = 0;



    //bool test = false;


    void Start()
    {
        seconds = PlayerPrefs.GetInt("CurrentSeconds", 0);
        minutes = PlayerPrefs.GetInt("CurrentMinutes", 0);
        tasksToDo = PlayerPrefs.GetInt("CurrentTasksToDo", 0);
        tasksCompleted = PlayerPrefs.GetInt("CurrentTasksCompleted", 0);
        timeBetweenTasks = PlayerPrefs.GetFloat("CurrentTimeBetweenTasks", 10f);
        DangerMeter.value = PlayerPrefs.GetFloat("CurrentDangerMeter", 0);

        gameStarted = true;
        redScreen.enabled = false;
    }

    void Update()
    {
        if (gameStarted)
        {
            StartCoroutine(TimerUI());
            StartCoroutine(DangerManager());
            StartCoroutine(TaskManager());
            gameStarted = false;
        }

        if (DangerMeter.value >= 0.8 && !inDangerZone)
        {
            inDangerZone = true;
            StartCoroutine(BlinkingRed());
            
        } 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("CurrentSeconds", 0);
            PlayerPrefs.SetInt("CurrentMinutes", 0);
            PlayerPrefs.SetFloat("CurrentTasksToDo", 0);
            PlayerPrefs.SetFloat("CurrentTasksCompleted", 0);
            PlayerPrefs.SetFloat("CurrentTimeBetweenTasks", 10f);
            PlayerPrefs.SetFloat("CurrentDangerMeter", 0);
            PlayerPrefs.Save();
        }
    }

    void DangerMeterIncrease()
    {
        baseDangerMeterIncrease += 0.001f * _tasksCompleted;
        
    }


    IEnumerator TimerUI()
    {
        while(gameEnded == false)
        {
            seconds++;
            
            if(seconds == 60)
            {
                minutes++;
                seconds = 0;
            }
           
            string timeString = "0" + minutes.ToString() + " : ";

            if (seconds < 10)
                timeString += "0" + seconds.ToString();
            else
                timeString += seconds.ToString();

            timerText.text = timeString;

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator DangerManager()
    {
        while (gameEnded == false)
        {

            if(tasksToDo != 0)
            {
                DangerMeter.value += baseDangerMeterIncrease;
            }
            else
            {
                DangerMeter.value -= 0.005f;
                if (DangerMeter.value < 0)
                    DangerMeter.value = 0;
            }


            yield return new WaitForSeconds(0.1f);



            if (DangerMeter.value >= DangerMeter.maxValue)
            {
                DangerMeter.value = DangerMeter.maxValue;
                gameEnded = true;
                SceneManager.LoadScene("GameOver");

            }           
        }
    }

    IEnumerator TaskManager()
    {
        int randomTaskMemory = 0;

        while (gameEnded == false)
        {
            int randomTask = Random.Range(1, 4);

            while (randomTask == randomTaskMemory)
            {
                randomTask = Random.Range(1, 4);
            }
            print(randomTask);
            randomTaskMemory = randomTask;

            if (randomTask == 1)
            {
                taskAsteroid.GetComponent<SpriteRenderer>().enabled = true;
                taskAsteroid.GetComponent<Collider>().enabled = true;
            }
            else if (randomTask == 2)
            {
                taskLaser.GetComponent<SpriteRenderer>().enabled = true;
                taskLaser.GetComponent<Collider>().enabled = true;
            }
            else if (randomTask == 3)
            {
                taskFlashlight.GetComponent<SpriteRenderer>().enabled = true;
                taskFlashlight.GetComponent<Collider>().enabled = true;
            }
            tasksToDo++;
            yield return new WaitForSeconds(timeBetweenTasks);
            timeBetweenTasks -= 0.5f;
        }
    }

    IEnumerator BlinkingRed()
    {
        foreach (GameObject fx in fireFx)
        {
            fx.SetActive(true);
        }
        while (gameEnded == false && inDangerZone)
        {
            redScreen.enabled = true;
            yield return new WaitForSeconds(0.7f);
            print("blink");
            redScreen.enabled = false;
            yield return new WaitForSeconds(0.7f);
        }
        inDangerZone = false;

        foreach (GameObject fx in fireFx)
        {
            fx.SetActive(false);
        }

    }
}


