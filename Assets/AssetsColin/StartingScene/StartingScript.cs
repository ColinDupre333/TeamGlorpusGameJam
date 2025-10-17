using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScript : MonoBehaviour
{
    [SerializeField] AudioClip MainMenuMusic;
    [SerializeField] float MusicVolume = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectPool.objectPoolInstance.RecreateObjects();
        PlayerPrefs.SetInt("CurrentSeconds", 0); // On reset les valeurs de la partie
        PlayerPrefs.SetInt("CurrentMinutes", 0);
        PlayerPrefs.SetFloat("CurrentTasksToDo", 0);
        PlayerPrefs.SetFloat("CurrentTasksCompleted", 0);
        PlayerPrefs.SetFloat("CurrentTimeBetweenTasks", 10f);
        PlayerPrefs.SetFloat("CurrentDangerMeter", 0);
        PlayerPrefs.Save();
        SFXManager.instance.PlaySFX(MainMenuMusic,transform.transform, MusicVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    { 
        SceneManager.LoadScene("MainGameScene");
    }
}
