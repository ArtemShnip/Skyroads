using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject score;
    public GameObject ship;
    private List<GameObject> asteroids = new List<GameObject>();
    private float speed ;
    private float asteroidDistace = 30f;
    public float asteroidMinDistance = 10f;
    public float timeLevelUp = 2f;
    public int maxAsteroidCount = 30;
    
    
    void Start()
    {
        ResetLevel();
        StartCoroutine("Level");
    }

    void Update()
    {
        if (ship == null) return;
        
        // Set the movement
        speed = ship.GetComponent<PlayerMovement>().speed;
        
        foreach (GameObject asteroid in asteroids)
        {
            asteroid.transform.position -= new Vector3(0,0,speed * Time.deltaTime);
        }

        // Remove an asteroid outside the camera and create the next one
        if (asteroids[0].transform.position.z < -10)
        {
            Destroy(asteroids[0]);
            score.GetComponent<Score>().AsteroidPassed();
            asteroids.RemoveAt(0);
            
            CreateNextAsteroid();
        }
    }
    
    public void ResetLevel()
    {
        while (asteroids.Count > 0)
        {
            Destroy(asteroids[0]);
            asteroids.RemoveAt(0);
        }

        for (int i = 0; i < maxAsteroidCount; i++)
        {
            CreateNextAsteroid();
        }
    }
    
    // Create the next asteroid
    private void CreateNextAsteroid()
    {
        Vector3 pos = new Vector3(0, 2.5f, 60f);
        if (asteroids.Count > 0 )
        {
            pos = asteroids[asteroids.Count - 1].transform.position + new Vector3(0, 0, asteroidDistace);
        }

        pos.x = Random.Range(-4,4);
        GameObject go = Instantiate(asteroidPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        asteroids.Add(go);
    }

    // Increase the difficulty. Reduce the distance of the asteroid after a given time
    private IEnumerator Level()
    {
        while (asteroidDistace > asteroidMinDistance)
        {
            asteroidDistace -= 1f;
            yield return new WaitForSeconds(timeLevelUp);
        }
    }
}
