using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    [SerializeField] AsteroidComponent asteroidPrefab;
    public int asteroidCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (asteroidCount == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnAsteroid();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (asteroidCount == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnAsteroid();
            }
        }
    }
    private void SpawnAsteroid()
    {
        float offset = Random.Range(0, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;
        int edge = Random.Range(0, 4);
        if (edge == 0)
        {
            viewportSpawnPosition = new Vector2(offset, 0f);

        }
        else if (edge == 1)
        {
            viewportSpawnPosition = new Vector2(offset, 1f);
        }
        else if (edge == 2)
        {
            viewportSpawnPosition = new Vector2(0f, offset);
        }
        else if (edge == 3)
        {
            viewportSpawnPosition = new Vector2(1f, offset);
        }
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        AsteroidComponent asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        asteroid.spaceManager = this;
    }
}
