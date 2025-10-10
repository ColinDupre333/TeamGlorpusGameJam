using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text timerText;
    [SerializeField] Slider DangerMeter;

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

    //task manager
    [Header("Tasks")]
    [SerializeField] public GameObject taskAsteroid;
    [SerializeField] public GameObject taskLaser;
    [SerializeField] public GameObject taskFlashlight;
    float timeBetweenTasks = 10f;

    bool gameStarted = false;
    public bool gameEnded = false;

    public int seconds = 0;
    public int minutes = 0;



    //bool test = false;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameStarted = true;
        DangerMeter.value = 0;
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
        //if (Input.GetKeyDown(KeyCode.Space) && !test)
        //{
        //    StartCoroutine(Testing());
        //}
    }

    void DangerMeterIncrease()
    {
        baseDangerMeterIncrease = 0.001f * _tasksCompleted;
        
    }

    //IEnumerator Testing()
    //{      
    //        test = true;
    //        tasksCompleted++;
    //        yield return new WaitForSeconds(3f);
    //        test = false;       
    //}


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
                taskAsteroid.SetActive(true);
            }
            else if (randomTask == 2)
            {
                taskLaser.SetActive(true);
            }
            else if (randomTask == 3)
            {
                taskFlashlight.SetActive(true);
            }
            tasksToDo++;
            yield return new WaitForSeconds(timeBetweenTasks);
            timeBetweenTasks -= 0.5f;
        }
    }

    IEnumerator BlinkingRed()
    {
        while(gameEnded == false && inDangerZone)
        {
            redScreen.enabled = true;
            yield return new WaitForSeconds(0.7f);
            redScreen.enabled = false;
            yield return new WaitForSeconds(0.7f);
        }
        inDangerZone = false;
    }
}


