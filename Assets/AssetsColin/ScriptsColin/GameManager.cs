using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] Slider DangerMeter;

    //UI
    Image sliderFiller;

    //DangerManager
    int tasksCompleted = 0;
    int tasksToDo = 0;


    bool gameStarted = false;
    public bool gameEnded = false;

    public int seconds = 0;
    public int minutes = 0;


    void Start()
    {

        sliderFiller = DangerMeter.fillRect.GetComponent<Image>();

        gameStarted = true;
        DangerMeter.value = 0;
    }

    void Update()
    {
        if (gameStarted)
        {
            StartCoroutine(TimerUI());
            gameStarted = false;
        }
        
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

            //if(DangerMeter.value < DangerMeter.maxValue)
            //    DangerMeter.value += 0.10f;

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator DangerManager()
    {
        while (gameEnded == false)
        {
            
            yield return new WaitForSeconds(1f);



            if (DangerMeter.value >= DangerMeter.maxValue)
            {
                gameEnded = true;

            }           
        }
    }
}


