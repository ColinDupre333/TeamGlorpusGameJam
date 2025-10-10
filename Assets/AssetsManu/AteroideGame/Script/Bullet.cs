using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletLifetime = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Destroy(gameObject, bulletLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
