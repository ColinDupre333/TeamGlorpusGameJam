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
            //SceneManager.LoadScene("Asteroid");

            //gameManager.tasksToDo--;
            //gameObject.SetActive(false);
        }
    }
}
