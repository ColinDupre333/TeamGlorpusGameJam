using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;



    bool gameStarted = false;
    bool gameEnded = false;

    int seconds = 0;
    int minutes = 0;


    void Start()
    {
        gameStarted = true;
    }

    // Update is called once per frame
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

            yield return new WaitForSeconds(1f);
        }
    }





}


