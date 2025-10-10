using UnityEngine;

public class TaskFlashlightS : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //start the flashlight task
            gameManager.tasksToDo--;
            gameObject.SetActive(false);
            
        }
    }
}
