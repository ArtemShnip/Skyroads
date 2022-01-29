using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject RoadPrefab;
    public GameObject ship;
    public GameObject score;
    private List<GameObject> roads = new List<GameObject>();
    private float speed ;
    public int maxRoadCount = 40;
    private bool _isBoost;
    
    void Start()
    {
        ResetLevel();
    }
    
    void Update()
    {
        if (ship == null) return;
        
        // Set the movement
        speed = ship.GetComponent<PlayerMovement>().speed;
        _isBoost = ship.GetComponent<PlayerMovement>().isBoost;

        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0,0,speed * Time.deltaTime);
        }

        // Remove the road outside the camera and create the next platform
        // But better to use a pool objects
        if (roads[0].transform.position.z < -20)
        {
            Destroy(roads[0]);
            
            // Add points for the traveled road 
            score.GetComponent<Score>().PlatformPassed(_isBoost);
            roads.RemoveAt(0);
            
            CreateNextRoad();
        }
    }

    // Create the next road platform
    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;
        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 10);
        }

        GameObject go = Instantiate(RoadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roads.Add(go);
    }

    public void ResetLevel()
    {
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }

        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
    }
}
