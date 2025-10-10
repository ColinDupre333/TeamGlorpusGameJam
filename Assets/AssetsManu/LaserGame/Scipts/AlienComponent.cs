using UnityEngine;

public class AlienComponent : MonoBehaviour
{
    [SerializeField] Transform[] Locations;
    [SerializeField] float speed = 2f;
    private int currentLocationIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float remainingDistance = Vector2.Distance(this.gameObject.transform.position, Locations[currentLocationIndex].position);
        while(remainingDistance >= 0.1f)
        {
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, Locations[currentLocationIndex].position, speed * Time.deltaTime);
        }
        currentLocationIndex = (currentLocationIndex + 1) % Locations.Length;
    }
}
