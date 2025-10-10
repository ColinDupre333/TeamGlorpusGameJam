using UnityEngine;

public class TaskLaserS : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //start the laser task
            gameManager.tasksToDo--;
            gameObject.SetActive(false);
            

        }
    }
}
