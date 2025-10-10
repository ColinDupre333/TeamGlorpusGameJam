using UnityEngine;

public class AsteroidComponent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //[SerializeField] GameObject manager;
    //[SerializeField] GameObject SpaceShip;
    public SpaceManager spaceManager;
    void Start()
    {
        // spaceShip  = SpaceShip.GetComponent<SpaceShip>();
        //spaceManager = manager.GetComponent<SpaceManager>();
        Rigidbody2D asteroidRigidBody = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float speed = Random.Range(2f, 5f);
        asteroidRigidBody.AddForce(direction * speed, ForceMode2D.Impulse);
        spaceManager.asteroidCount++;
        Debug.Log(spaceManager.asteroidCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            spaceManager.score = spaceManager.score + 1;
            Debug.Log("Score: " + spaceManager.score);
            Destroy(collision.gameObject);
            spaceManager.asteroidCount--;
            Destroy(gameObject);
            Debug.Log(spaceManager.asteroidCount);
        }
        
    }

   

}
