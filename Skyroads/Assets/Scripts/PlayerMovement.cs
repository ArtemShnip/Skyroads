using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float speed = 20f;
    public bool isBoost;
    private Rigidbody _rigidbody;
    private static float horizontalAxis;
    public GameObject сamera;
    private ParticleSystem[] particleSystems;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        horizontalAxis = 0;
        particleSystems = GetComponentsInChildren<UnityEngine.ParticleSystem>();
        isBoost = false;
    }

    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalAxis,0);
        
        // Boost 
        if (Input.GetKey(KeyCode.Space))
        {
            speed = 40f;
            moveSpeed = 10f;
            isBoost = true;
            
            // Change the code in SmoothFollow for smooth camera movement on a distance
            сamera.GetComponent<SmoothFollow>().distance = 5;
            сamera.GetComponent<SmoothFollow>().height = 2;
             
            // Increase ParticleSystem lifetime ThrusterFX
            for (int i = 0; i < particleSystems.Length; i++)
            {
                var mainModule = particleSystems[i].main;
                mainModule.startLifetime = 0.5f;
            }
        }
        else
        {
            speed = 20;
            moveSpeed = 5f;
            isBoost = false;
            
            сamera.GetComponent<SmoothFollow>().distance = 10;
            сamera.GetComponent<SmoothFollow>().height = 5;
            
            // Reduce ParticleSystem lifetime ThrusterFX
            for (int i = 0; i < 4; i++)
            {
                var mainModule = particleSystems[i].main;
                mainModule.startLifetime = 0.3f;
            }
        }
        
        Move(direction);
    }

    private void Move(Vector3 direction)
    {
        // Movement restriction on the x-axis
        if (gameObject.transform.position.x > 4.5f && direction.x >= 0 ||
            gameObject.transform.position.x < -4.5f && direction.x <= 0)
        {
            direction.x = 0;
        }
        
        // x-axis movements
        Vector3 velocity = _rigidbody.velocity;
        velocity.x = direction.x * moveSpeed ;
        velocity.y = 0;
        _rigidbody.velocity = velocity;

        //spaceship rotation
        Quaternion rotation = gameObject.transform.rotation;
        rotation.z = -direction.x * 0.4f;
        gameObject.transform.rotation = rotation;
    }

    // Asteroid collision trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Asteroid"))
        {
            Destroy(gameObject);
            GameMenu.isGameOver = true;
        }
    }
}