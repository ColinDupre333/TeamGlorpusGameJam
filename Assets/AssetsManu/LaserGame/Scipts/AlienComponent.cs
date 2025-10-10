using UnityEngine;

public class AlienComponent : MonoBehaviour
{
    [SerializeField] Transform[] Locations;
    [SerializeField] float speed = 2f;
    private int currentLocationIndex = 0;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
        float remainingDistance = Vector2.Distance(this.gameObject.transform.position, Locations[currentLocationIndex].position);
        if(remainingDistance >= 0.1f)
        {
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, Locations[currentLocationIndex].position, speed * Time.deltaTime);
        }
        else
            currentLocationIndex = (currentLocationIndex + 1) % Locations.Length;
    }
}
