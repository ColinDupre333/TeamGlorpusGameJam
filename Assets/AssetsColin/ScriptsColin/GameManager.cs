using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] Slider DangerMeter;

    //UI
    [SerializeField] Image redScreen;

    //DangerManager
    
    float _tasksCompleted;
    float tasksToDo = 1;
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


    bool gameStarted = false;
    public bool gameEnded = false;

    public int seconds = 0;
    public int minutes = 0;



    //bool test = false;


    void Start()
    {
        gameStarted = true;
        DangerMeter.value = 0.7f;
        redScreen.enabled = false;
    }

    void Update()
    {
        if (gameStarted)
        {
            StartCoroutine(TimerUI());
            StartCoroutine(DangerManager());
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
        print(baseDangerMeterIncrease);
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
                DangerMeter.value -= 0.05f;
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


