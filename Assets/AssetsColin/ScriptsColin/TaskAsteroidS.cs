using UnityEngine;

public class TaskAsteroidS : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //start the asteroid task
            gameManager.tasksToDo--;
            gameObject.SetActive(false);
        }
    }
}
