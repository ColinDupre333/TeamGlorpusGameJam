using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{

    [SerializeField] Transform Gun;
    [SerializeField] TMPro.TextMeshProUGUI Finito;
    [SerializeField] AudioClip laserSound;
    [SerializeField] float soundVolume = 0.2f;
    SoundPlayerScript soundPlayer;
    public LayerMask layersToHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectPool.objectPoolInstance.RecreateObjects();
        soundPlayer = GetComponent<SoundPlayerScript>();
        Finito.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        soundPlayer.PlaySound(laserSound, transform, soundVolume);
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 50f, layersToHit);
        if (hit.collider == null)
        {
            transform.localScale = new Vector3(50f, transform.localScale.y, 1);
            return;
        }
        
        if(hit.collider.tag == "UVO")
        {
            transform.localScale = new Vector3((hit.collider.transform.position.x - Gun.transform.position.x) / 2.3f, transform.localScale.y, 1);
        }
        if (hit.collider.tag == "Ennemi")
        {
            transform.localScale = new Vector3((hit.collider.transform.position.x - Gun.transform.position.x ) /2.3f , transform.localScale.y, 1);
            Destroy(hit.collider.gameObject);
            Finito.enabled = true;

            float tasksToDo = PlayerPrefs.GetFloat("CurrentTasksToDo", 0);
            float tasksCompleted = PlayerPrefs.GetFloat("CurrentTasksCompleted", 0);

            PlayerPrefs.SetFloat("CurrentTasksToDo", tasksToDo--);
            PlayerPrefs.SetFloat("CurrentTasksCompleted", tasksCompleted + 1f);
            PlayerPrefs.Save();

            StartCoroutine(WaitAndGoBack());

        }
    }

    IEnumerator WaitAndGoBack()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainGameScene");
    }
}
