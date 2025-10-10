using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShip : MonoBehaviour
{
    [Header("Space ship params")]
   
    [SerializeField] float shipMaxVelocity = 10f;
    [SerializeField] float shipAcceleration = 10f; 
    [SerializeField] float shipRotationSpeed = 180f;
    [SerializeField] float bulletSpeed = 10f;
    Rigidbody2D shipRigidBody;
    bool isAlive = true;
    bool isAccelerating = false;
    
    public int points2CompleteTask = 15;

    [Header("Object references")]
    [SerializeField] Transform bulletsSpawn;
    [SerializeField] Rigidbody2D bulletPrefab;
    void Awake()
    {
        shipRigidBody = GetComponent<Rigidbody2D>();
    
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            HandleShipAcceleration();
            HandleShipRotation();
            HandleShooting();
        }
       
        
    }

    private void FixedUpdate()
    {
        if (isAlive && isAccelerating)
        {
            shipRigidBody.AddForce(shipAcceleration * transform.up);
            shipRigidBody.linearVelocity = Vector2.ClampMagnitude(shipRigidBody.linearVelocity, shipMaxVelocity);
        }
    }

    private void HandleShipAcceleration()
    {
        isAccelerating = Input.GetKey(KeyCode.W);
    }

    private void HandleShipRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(shipRotationSpeed * Time.deltaTime * transform.forward);

        }
        else if (Input.GetKey(KeyCode.D))
        { 
            transform.Rotate(-shipRotationSpeed * Time.deltaTime * transform.forward);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody2D bulletInstance = Instantiate(bulletPrefab, bulletsSpawn.position,shipRigidBody.transform.rotation);
            Vector2 shipVelocity = shipRigidBody.linearVelocity;
            Vector2 shipDirection = transform.up;
            float shipFowardSpeed = Vector2.Dot(shipVelocity, shipDirection);

            if(shipFowardSpeed < 0)
            {
                shipFowardSpeed = 0;
            }
            bulletInstance.linearVelocity = shipDirection * shipFowardSpeed;
            bulletInstance.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Astroid"))
        {
            isAlive = false;
            shipRigidBody.linearVelocity = Vector2.zero;

            float tasksToDo = PlayerPrefs.GetFloat("CurrentTasksToDo", 0);
            float tasksCompleted = PlayerPrefs.GetFloat("CurrentTasksCompleted", 0);
            float dangerMeterVal = PlayerPrefs.GetFloat("CurrentDangerMeter", 0);

            PlayerPrefs.SetFloat("CurrentTasksToDo", tasksToDo--);
            PlayerPrefs.SetFloat("CurrentTasksCompleted", tasksCompleted + 1f);
            PlayerPrefs.SetFloat("CurrentDangerMeter", dangerMeterVal + 0.2f);
            PlayerPrefs.Save();


            SceneManager.LoadScene("MainGameScene");
            gameObject.SetActive(false);
        }
    }
}
